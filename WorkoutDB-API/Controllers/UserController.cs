using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkoutDataAccess;

namespace WorkoutDB_API.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public IEnumerable<UserPerm> Get()
        {
            using (WorkoutEntities entities = new WorkoutEntities())
            {
                return entities.UserPerms.ToList();
            }
        }

        //[HttpGet]
        public UserPerm Get(int id)
        {
            using (WorkoutEntities entities = new WorkoutEntities())
            {
                return entities.UserPerms.FirstOrDefault(e => e.UserID == id);
            }
        }

        public IHttpActionResult Put(UserPerm user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (WorkoutEntities entities = new WorkoutEntities())
            {
                var existingUser = entities.UserPerms.Where(s => s.UserID == user.UserID)
                                                        .FirstOrDefault<UserPerm>();

                if (existingUser != null)
                {
                    existingUser.Username = user.Username;
                    existingUser.Password = user.Password;
                    existingUser.PaidUser = user.PaidUser;
                    existingUser.CreateDate = user.CreateDate;
                    existingUser.LastUser = user.LastUser;
                    existingUser.Height = user.Height;
                    existingUser.Weight = user.Weight;
                    existingUser.Lbs_Kilos = user.Lbs_Kilos;
                    existingUser.Email = user.Email;

                   entities.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        //Get action methods of the previous section
        public IHttpActionResult PostNewStudent(UserPerm user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (WorkoutEntities entities = new WorkoutEntities())
            {
                entities.UserPerms.Add(new UserPerm()
                {
                Username = user.Username,
                Password = user.Password,
                PaidUser = user.PaidUser,
                CreateDate = user.CreateDate,
                LastUser = user.LastUser,
                Height = user.Height,
                Weight = user.Weight,
                Lbs_Kilos = user.Lbs_Kilos,
                Email = user.Email
            });

                entities.SaveChanges();
            }

            return Ok();
        }

    }
}
