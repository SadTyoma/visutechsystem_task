using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task.Models;

namespace task.Controllers
{
    public class UsersController
    {
        IUsersView _view;
        IList _users;
        User _selectedUser;

        public UsersController(IUsersView view, IList users)
        {
            _view = view;
            _users = users;
            view.SetController(this);
        }

        public IList Users
        {
            get { return _users; }
        }
        public void updateViewDetailValues(User user)
        {
            _view.FirstName = user.FirstName;
            _view.LastName = user.LastName;
            _view.ID = user.ID;
            _view.Age = user.Age;
            _view.Sexofperson = user.Sexofperson;
        }

        public void updateUserWithViewValues(User user)
        {
            user.FirstName = _view.FirstName;
            user.LastName = _view.LastName;
            user.ID = _view.ID;
            user.Age = _view.Age;
            user.Sexofperson = _view.Sexofperson;
        }

        public void LoadView()
        {
            _view.ClearGrid();
            if (_users.Count != 0)
            {
                foreach (User user in _users)
                    _view.AddUserToGrid(user);
                _view.SetSelectedUserInGrid((User)_users[0]);
            }
            else
                ClearView();
        }

        public void SelectedUserChanged(string selectedUserId)
        {
            foreach (User user in _users)
            {
                if (user.ID == selectedUserId)
                {
                    _selectedUser = user;
                    updateViewDetailValues(user);
                    _view.SetSelectedUserInGrid(user);
                    break;
                }
            }
        }

        public void ClearView()
        {
            _selectedUser = new User("", "", "", "", User.SexOfPerson.Male);
            updateViewDetailValues(_selectedUser);
        }

        public void RemoveUser()
        {
            string id = _view.GetIdOfSelectedUserInGrid();
            User userToRemove = null;

            if (id != "")
            {
                foreach (User user in _users)
                {
                    if (user.ID == id)
                    {
                        userToRemove = user;
                        break;
                    }
                }

                if (userToRemove != null)
                {
                    int newSelectedIndex = _users.IndexOf(userToRemove);
                    _users.Remove(userToRemove);
                    _view.RemoveUserFromGrid(userToRemove);

                    if (newSelectedIndex > -1 && newSelectedIndex < _users.Count)
                    {
                        _view.SetSelectedUserInGrid((User)_users[newSelectedIndex]);
                    }
                }
            }
        }
        public bool CheckIDInArray()
        {
            foreach (User u in _users)
            {
                if (_selectedUser.ID == u.ID)
                    return false;
            }
            return true;
        }
        public void Save()
        {
            updateUserWithViewValues(_selectedUser);
            if (!_users.Contains(_selectedUser))
            {
                if (CheckIDInArray())
                {
                    _users.Add(_selectedUser);
                    _view.AddUserToGrid(_selectedUser);
                    _view.SetSelectedUserInGrid(_selectedUser);
                    ClearView();
                }
                else
                    MessageBox.Show("This ID already exists!", "Error.");
            }
            else
            {
                _view.UpdateGridWithChangedUser(_selectedUser);
            }
        }
    }
}
