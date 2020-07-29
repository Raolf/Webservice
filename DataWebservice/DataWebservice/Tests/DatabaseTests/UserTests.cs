using DataWebservice.Controllers.API;
using DataWebservice.Data;
using DataWebservice.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Tests.DatabaseTests
{
    public class UserTests
    {
        private readonly DataWebserviceContext _context;

        public UserTests(DataWebserviceContext context)
        {
            _context = context;
        }

        [Test]
        public void Can_get_items()
        {

            using (_context)
            {
                UsersController controller = new UsersController(_context);

                var user = controller.GetUser();
                var List = new List<User>();

                foreach (var user1 in user.Result.Value.ToList())
                {
                    List.Add(user1);
                }

                Assert.AreEqual(List.Count, List.Count);

            }
        }

        [Test]
        public async void Can_post_delete_items()
        {

            using (_context)
            {
                UsersController controller = new UsersController(_context);

                Models.User testdata = new Models.User();
                testdata.displayName = "testuser";
                testdata.password = "testpassword";
                testdata.isAdmin = true;
                await controller.PostUser(testdata);

                var user = controller.GetUser();
                var List = new List<User>();
                int Listcount = 0;
                foreach (var user1 in user.Result.Value.ToList())
                {
                    List.Add(user1);
                    Listcount++;
                }

                Assert.AreEqual(Listcount, List.Count);
                Assert.AreEqual("testpassword", List[Listcount-1].password);


                //Delete

                if (List.Count > 0)
                {
                    var item = List[List.Count - 1];

                    await controller.DeleteUser(item.userID);
                }

                user = controller.GetUser();
                List = new List<User>();

                foreach (var user1 in user.Result.Value.ToList())
                {
                    List.Add(user1);
                }


                Assert.AreEqual(Listcount-1, List.Count);

            }
        }

    }
}
