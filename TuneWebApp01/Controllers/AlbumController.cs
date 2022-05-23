using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using TuneWebApp01.Models;
//using System.Data.SqlClient;

namespace TuneWebApp01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //private readonly IWebHostEnvironment _env;
        public AlbumController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select AlbumId, AlbumName from
                            dbo.Album
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

            //return new JsonResult("Get Successfully!");
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Album album)
        {
            string query = @"
                            insert into dbo.Album
                            values (@AlbumName)
                            ";
            string sqlDataSource = _configuration.GetConnectionString("TuneAppCon");

            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@AlbumName", album.AlbumName);
                    //myCommand.Parameters.AddWithValue("@Tune", album.Tune);
                    //myCommand.Parameters.AddWithValue("@DateOfJoining", album.DateOfJoining);
                    //myCommand.Parameters.AddWithValue("@PhotoFileName", album.PhotoFileName);

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
        public JsonResult Put(Album album)
        {
            string query = @"
                            update dbo.Album
                            set AlbumName=@AlbumName
                            where AlbumId=@AlbumId
                            ";
            string sqlDataSource = _configuration.GetConnectionString("TuneAppCon");

            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@AlbumId", album.AlbumId);
                    myCommand.Parameters.AddWithValue("@AlbumName", album.AlbumName);
                    //myCommand.Parameters.AddWithValue("@Tune", album.Tune);
                    //myCommand.Parameters.AddWithValue("@DateOfJoining", album.DateOfJoining);
                    //myCommand.Parameters.AddWithValue("@PhotoFileName", album.PhotoFileName);

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
                            delete from dbo.Album
                            where AlbumId=@AlbumId
                            ";
            string sqlDataSource = _configuration.GetConnectionString("TuneAppCon");

            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@AlbumId", id);
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

        //[Route("SaveFile")]
        //[HttpPost]
        //public JsonResult SaveFile()
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;
        //        var physicalPath = _env.ContentRootPath +"/Photos/" + filename;

        //        using(var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            postedFile.CopyTo(stream);
        //        }
        //        return new JsonResult(filename);
        //    }
        //    catch (Exception)
        //    {
        //        return new JsonResult("anonymous.png");
        //    }
        //}
    }
}
