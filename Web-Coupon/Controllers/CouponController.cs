﻿using Microsoft.AspNetCore.Mvc;
using MinimalAPI_Coupon.Models.DTOs;
using Newtonsoft.Json;
using Web_Coupon.Models;
using Web_Coupon.Services;

namespace Web_Coupon.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
           this._couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO> list = new List<CouponDTO>();
          var response =  await _couponService.GetAllCoupon<ResponsDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            CouponDTO cDTO = new CouponDTO();
            var response = await _couponService.GetCouponById<ResponsDto>(id);
            if (response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.CreateCouponAsync<ResponsDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateCoupon(int couponId)
        {
            var response = await _couponService.GetCouponById<ResponsDto>(couponId);
            if (response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoupon(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.UpdateCouponAsync<ResponsDto>(model);
                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(model);
        }


        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            var response = await _couponService.GetCouponById<ResponsDto>(couponId);
            if(response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.DeleteCouponAsync<ResponsDto>(model.Id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return NotFound();
        }
       
    }
}
