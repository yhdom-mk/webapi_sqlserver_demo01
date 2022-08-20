using Xunit;

namespace TuneApi.Moq
{
    public class TuneControllerTest
    {
        //private readonly TuneController _controller;
        //private readonly

        public TuneControllerTest()
        {
            //_controller = new TuneController();
        }
        [Fact]

        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Act
            //var okResult = _controller.Get();

            //Assert
            //Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        public void Get_WhenCalled_ReturnsAllTunes() 
        {
            //Act
            //var okResult = _controller.Get() as OkObjectResult;

            //Assert
            //var tune = Assert.IsType<List<OkObjectResult>>(okResult.Value);
            //Assert.Equal(1, tune.Count);
        }
    }
}