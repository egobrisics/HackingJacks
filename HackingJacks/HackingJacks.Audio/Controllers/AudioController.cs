﻿using HackingJacks.Audio.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HackingJacks.Controllers
{
    [Route("audio")]
    public class AudioController : Controller
    {
        private IAudioService _audioService;

        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            var result = _audioService.Get(id);

            if (!result.Success)
            {
                return Json(result.Error.ToString());
            }

            return Json(result.Item);
        }
      
    }
}
