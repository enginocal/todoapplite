using xUnit;

namespace todoapplite.UnitTests.Unit.Controllers
{
    public class UserControllerTests
    {

        [Fact]
        public void usercontroller_register_first_should_return_ok()
        {
            var controller = new UserController();
            // var result=controller.
            var user = new User()
            {
                Email = Guid.NewGuid().ToString() + "@hotmail.com",
                Password = "1234",
                Name = "merhaba"
            };

            var result = controller.Register(user);
            
        }
    }
}