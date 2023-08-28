using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI_Coupon;
using MinimalAPI_Coupon.Data;
using MinimalAPI_Coupon.Models;
using MinimalAPI_Coupon.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/coupons", () =>
{
    APIResponse response = new APIResponse();

    response.Result = CouponStore.couponlist;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;

    return Results.Ok(response);
}).WithName("GetCoupons").Produces(200);

app.MapGet("/api/coupon/{id:int}", (int id) =>
{
    APIResponse response = new APIResponse();
    response.Result = CouponStore.couponlist.FirstOrDefault(c => c.Id == id);
    response.IsSuccess = true;
    response.StatusCode=System.Net.HttpStatusCode.OK;


    return Results.Ok(response);
}).WithName("GetCoupon");

//app.MapPost("/api/coupon", ([FromBody] Coupon coupon) =>
//{
//    if(coupon.Id !=0 || string.IsNullOrEmpty(coupon.Name))
//    {
//        return Results.BadRequest("Invalid ID or Coupon Name");
//    }
//    if(CouponStore.couponlist.FirstOrDefault(c => c.Name.ToLower() == coupon.Name.ToLower()) != null)
//    {
//        return Results.BadRequest("Coupon Name already Exists");
//    }

//    coupon.Id = CouponStore.couponlist.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
//    CouponStore.couponlist.Add(coupon);
//    return Results.Created($"/api/coupon/{coupon.Id}", coupon);
//}).Produces<Coupon>(201).Produces(400);


app.MapPost("/api/coupon", async (
    IValidator<CouponCreateDTO> validator, 
    IMapper _mapper, 
    [FromBody] CouponCreateDTO coupon_C_DTO) =>
{
    APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    var validationResult = await validator.ValidateAsync(coupon_C_DTO);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(response);
    }

    if (CouponStore.couponlist.FirstOrDefault(c => c.Name.ToLower() == coupon_C_DTO.Name.ToLower()) != null)
    {
        response.ErrorMessages.Add("Coupon Name already Exists");
        return Results.BadRequest(response);

    }


    // Utan Automapper 
    //Coupon coupon = new Coupon
    //{
    //    IsActive = coupon_C_DTO.IsActive,
    //    Name = coupon_C_DTO.Name,
    //    Percent = coupon_C_DTO.Precent
    //};

    // med AUTOMApper
    Coupon coupon = _mapper.Map<Coupon>(coupon_C_DTO);

    coupon.Id = CouponStore.couponlist.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
    CouponStore.couponlist.Add(coupon);


    CouponDTO couponDTO = _mapper.Map<CouponDTO>(coupon);

    response.Result = couponDTO;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.Created;
    return Results.Ok(response);

}).WithName("CreateCoupon").Accepts<CouponCreateDTO>("application/json").Produces<APIResponse>(201).Produces(400);

app.MapPut("/api/coupon/",async (
    IMapper _mapper,
    IValidator<CouponUpdateDTO> _validator,
    [FromBody]CouponUpdateDTO coupon_U_DTO) =>
{
    APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    //Add validation 
    var validatResult =  await _validator.ValidateAsync(coupon_U_DTO);

    if (!validatResult.IsValid)
    {
        response.ErrorMessages.Add(validatResult.Errors.FirstOrDefault().ToString());
    }

    Coupon couponFromStore = CouponStore.couponlist.FirstOrDefault(c => c.Id == coupon_U_DTO.Id);
    couponFromStore.IsActive = coupon_U_DTO.IsActive;
    couponFromStore.Name = coupon_U_DTO.Name;
    couponFromStore.Precent = coupon_U_DTO.Percent;
    couponFromStore.LastUpdate = DateTime.Now;


    Coupon coupon = _mapper.Map<Coupon>(coupon_U_DTO);


    response.Result = _mapper.Map<CouponDTO>(couponFromStore);

    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;

    return Results.Ok(response);

}).WithName("UpdateCoupon").Accepts<CouponUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400);

app.MapDelete("/api/coupon/{id:int}", (int id) =>
{
    APIResponse response = new APIResponse() { IsSuccess = false , StatusCode = System.Net.HttpStatusCode.BadRequest};

    Coupon couponFromStore = CouponStore.couponlist.FirstOrDefault(c => c.Id == id);

    if (couponFromStore != null)
    {
        CouponStore.couponlist.Remove(couponFromStore);
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.NoContent;
        return Results.Ok(response);
    }
    else
    {
        response.ErrorMessages.Add("Invalid ID");
        return Results.BadRequest(response);
    }

}).WithName("DeleteCoupon");

app.Run();


