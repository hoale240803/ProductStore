//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ProductStore.API.DBFirst.DataModels;

//namespace ProductStore.API.DBFirst.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MediaController : ControllerBase
//    {
//        private readonly StoreContext _context;

//        public MediaController(StoreContext context)
//        {
//            _context = context;

//        }

//        // GET: api/Media
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Media>>> GetMedias()
//        {
//            return await _context.Medias.ToListAsync();
//        }

//        // GET: api/Media/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Media>> GetMedia(int id)
//        {
//            var media = await _context.Medias.FindAsync(id);

//            if (media == null)
//            {
//                return NotFound();
//            }

//            return media;
//        }

//        // PUT: api/Media/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutMedia(int id, Media media)
//        {
//            if (id != media.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(media).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!MediaExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Media
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Media>> AddFile(Media media)
//        {

//            try
//            {

//                _context.Medias.Add(media);
//                // synchronnize db
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {

//            }

//            //return CreatedAtAction("GetMedia", new { id = media.Id }, media);

//            return CreatedAtAction("Created Images", new { images = media });
//        }



//        [HttpPost("uploadImages")]
//        public async Task<ActionResult> UploadImages(List<IFormFile> files)
//        {
//            long size = files.Count;

//            if (size == 0)
//            {
//                return Content("no file selected");
//            }

//            // full path to file in temp location
//            var folderName = Path.Combine("Resources", "Images");
//            var folderImages = Path.Combine(Directory.GetCurrentDirectory(), folderName);
//            try
//            {
//                foreach (var formFile in files)
//                {
//                    //get path to images folder
//                    //var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
//                    // get absolute path to image file
//                    var fullPath = Path.Combine(folderImages, GetUniqueFileName(formFile.FileName));
//                    //formFile.CopyTo(new FileStream(fullPath, FileMode.Create));
//                    var task1 = saveToStorage(files, folderImages);

//                    // save image's info into db
//                    var task2 = saveToDB(files, folderImages);

//                    await Task.WhenAll(task1, task2);
//                }

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//            }

//            return Ok(new { count = size, path = folderImages });
//        }
//        async Task saveToStorage(List<IFormFile> files, string folderImages)
//        {

//            foreach (var formFile in files)
//            {
//                //get path to images folder
//                //var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
//                // get absolute path to image file
//                var fullPath = Path.Combine(folderImages, GetUniqueFileName(formFile.FileName));

//                using (var stream = new FileStream(fullPath, FileMode.Create))
//                {
//                    await formFile.CopyToAsync(stream);
//                }
//            }

//        }
//        async Task saveToDB(List<IFormFile> files, string folderImages)
//        {

//            foreach (var formFile in files)
//            {
//                var fullPath = Path.Combine(folderImages, GetUniqueFileName(formFile.FileName));
//                Media image = new Media();
//                image.FileId = fullPath;
//                image.Name = formFile.Name;

//                // avoid foreign key
//                image.IdProduct = 0;
//                image.IdEmployee = 0;
//                image.IdExternalShipper = 0;
//                image.IdInternalShipper = 0;
//                await _context.Medias.AddAsync(image);
//                await _context.SaveChangesAsync();
//            }


//        }


//        private string GetUniqueFileName(string fileName)
//        {
//            fileName = Path.GetFileName(fileName);
//            return Path.GetFileNameWithoutExtension(fileName)
//                      + "_"
//                      + Guid.NewGuid().ToString().Substring(0, 4)
//                      + Path.GetExtension(fileName);
//        }

//        // DELETE: api/Media/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteMedia(int id)
//        {
//            var media = await _context.Medias.FindAsync(id);
//            if (media == null)
//            {
//                return NotFound();
//            }

//            _context.Medias.Remove(media);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // DELETE: api/Media/5
//        [HttpDelete("{id}/delete")]
//        public async Task<IActionResult> DeleteMedia1(int id)
//        {
//            var media = await _context.Medias.FindAsync(id);
//            if (media == null)
//            {
//                return NotFound();
//            }

//            _context.Medias.Remove(media);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool MediaExists(int id)
//        {
//            return _context.Medias.Any(e => e.Id == id);
//        }
//    }
//}
