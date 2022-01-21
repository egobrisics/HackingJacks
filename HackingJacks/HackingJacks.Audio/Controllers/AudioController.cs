using HackingJacks.Audio.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HackingJacks.Audio.Controllers
{
    [Route("audio")]
    public class AudioController : Controller
    {
        private IAudioService _audioService;
        private readonly string wwwRootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index(IFormFile myFile)
        {
            if (myFile != null)
            {

                var path = Path.Combine(wwwRootDirectory, DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile.FileName));

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    //await myFile.CopyToAsync(stream);
                    await _audioService.SaveAsync(stream);
                    var fileInfo = new FileInfo(path);
                    fileInfo.Delete();
                }
                return RedirectToAction("Index");
            }

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

        [HttpPost("{SaveAsync}")]
        public JsonResult SaveAsync(Stream file)
        {
            var result = _audioService.SaveAsync(file).Result;

            if (!result.Success)
            {
                return Json(result.Error.ToString());
            }

            return Json(result.Item);
        }

        [HttpPost("{Save}")]
        public JsonResult Save(Stream file)
        {
            var result = _audioService.SaveAsync(file).Result;

            if (!result.Success)
            {
                return Json(result.Error.ToString());
            }

            return Json(result.Item);
        }

    }
}
