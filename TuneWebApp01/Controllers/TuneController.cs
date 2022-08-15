using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using TuneWebApp01.Models;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using System.Data.SqlClient;

namespace TuneWebApp01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    public class TuneController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public TuneController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //public JsonConverterAttribute(){}

        //private string ConvertDataTableasJSON(DataTable dtable)
        //{
        //    return JsonConvert.SerializeObject(dtable);
        //}

        //public string DataTableToJsonWithJsonNet(DataTable tdata)
        //{
        //    return JsonConvert.SerializeObject(tdata);
        //}

        [HttpGet]
        public JsonResult Get()
        //public string Get()
        {
            string query = @"
                            select
                            TuneId, TuneName, Album,
                            convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                            photoFileName
                            from
                            dbo.Tune
                            ";
            string sqlDataSource = _configuration.GetConnectionString("TuneAppCon");

            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            JsonConvert.SerializeObject(table);
            //var jsonobj = ConvertDataTableasJSON(table);
            //var jsonobj = DataTableToJsonWithJsonNet(table);
            //string jsonString = JsonSerializer.Serialize(table);

            //return JsonConvert.DeserializeObject<JsonResult>(myReader.ToString());
            //return new JsonResult("Get Successfully!");
            return new JsonResult(table);

            //var tunes = new List<Tune> 
            //{
            //    new Tune { TuneId = 1, TuneName = "Come Together"},
            //    new Tune { TuneId = 2, TuneName = "Something"}
            //};

            //var album = new Album
            //{
            //    AlbumId = 1,
            //    AlbumName = "Abbey Road",
            //    DateOfJoining = "1969-9-26",
            //    PhotoFileName = "anonymous.png",
            //    //Tune = tunes
            //};

            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //};
            //return JsonSerializer.Serialize(album, options);
        }

        [HttpPost]
        public JsonResult Post(Tune tune)
        {
            string query = @"
                            insert into dbo.Tune
                            (TuneName, Album, DateOfJoining, PhotoFileName)
                            values (@TuneName, @Album, @DateOfJoining, @PhotoFileName)
                            ";
            string sqlDataSource = _configuration.GetConnectionString("TuneAppCon");

            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    //myCommand.Parameters.AddWithValue("@TuneId", tune.TuneId);
                    myCommand.Parameters.AddWithValue("@TuneName", tune.TuneName);
                    myCommand.Parameters.AddWithValue("@Album", tune.Album);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", tune.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", tune.PhotoFileName);
                    //myCommand.Parameters.AddWithValue("@TuneName", tune.TuneName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            JsonConvert.SerializeObject(table);

            //return new JsonResult("Post Successfully!!");
            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult Put(Tune tune)
        {
            string query = @"
                            set identity_insert dbo.Tune on
                            update dbo.Tune
                            set 
                            TuneId =@TuneId,
                            TuneName =@TuneName,
                            Album =@Album,
                            DateOfJoining =@DateOfJoining,
                            PhotoFileName =@PhotoFileName
                            where 
                            TuneId =@TuneId
                            set identity_insert dbo.Tune off
                            "; //where TuneId =@TuneId
            string sqlDataSource = _configuration.GetConnectionString("TuneAppCon");

            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@TuneId", tune.TuneId);
                    myCommand.Parameters.AddWithValue("@TuneName", tune.TuneName);
                    myCommand.Parameters.AddWithValue("@Album", tune.Album);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", tune.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", tune.PhotoFileName);
                    //myCommand.Parameters.AddWithValue("@TuneId", tune.TuneId);
                    //myCommand.Parameters.AddWithValue("@TuneName", tune.TuneName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            JsonConvert.SerializeObject(table);

            //return new JsonResult("Update Successfully!!!");
            return new JsonResult(table);
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Tune
                            where TuneId=@TuneId
                            ";
            string sqlDataSource = _configuration.GetConnectionString("TuneAppCon");

            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@TuneId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            JsonConvert.SerializeObject(table);

            //return new JsonResult("Delete Successfully.");
            return new JsonResult(table);
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        public void GetImageFile(object sender, EventArgs e)
        {
            //string iconPath = @"C:\test\image.ico";
            //System.Drawing.Icon icon = new System.Drawing.Icon(iconPath, 48, 48);
            ////Bitmapに変換
            //System.Drawing.Bitmap bmp = icon.ToBitmap();
            //icon.Dispose();
            //FileStream fileStream = File.OpenRead(@"C:\test\image.jpg");

            //Image.FromFile
            //System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\test\image.jpg");

            //Bitmap
            //System.Drawing.Bitmap btp = new System.Drawing.Bitmap(@"C:\test\image.jpg");

            //get programFolder
            var programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            Console.WriteLine(programFilesPath);
        }
    }
}
