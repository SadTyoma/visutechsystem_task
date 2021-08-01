using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task.Models;

namespace task.Controllers
{
    public interface IUsersView
    {
        void SetController(UsersController controller);
        void ClearGrid();
        void AddUserToGrid(User user);
        void UpdateGridWithChangedUser(User user);
        void RemoveUserFromGrid(User user);
        string GetIdOfSelectedUserInGrid();
        void SetSelectedUserInGrid(User user);

        string FirstName { get; set; }
        string LastName { get; set; }
        string ID { get; set; }
        string Age { get; set; }
        User.SexOfPerson Sexofperson { get; set; }
    }
}
