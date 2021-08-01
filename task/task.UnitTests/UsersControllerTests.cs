using NUnit.Framework;
using Moq;
using task.Controllers;
using System.Collections;
using task.Models;
using System.Collections.Generic;

namespace task.UnitTests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUsersView> _view;
        private IList _users = new List<User>();
        private UsersController _controller;

        [SetUp]
        public void Setup()
        {
            _view = new Mock<IUsersView>();
            _controller = new UsersController(_view.Object, _users);
        }
        [Test]
        public void RemoveUser_UserFound_Removed()
        {
            _view.Setup(v => v.GetIdOfSelectedUserInGrid()).Returns("1");
            _users.Add(new User("a", "a", "1", "1", User.SexOfPerson.Male));

            _controller.RemoveUser();
            int UsersCount = _users.Count;

            Assert.That(UsersCount, Is.EqualTo(0));
        }
        [Test]
        public void RemoveUser_UserNotFound_Nothing()
        {
            _users.Add(new User("a", "a", "1", "1", User.SexOfPerson.Male));
            _view.Setup(v => v.GetIdOfSelectedUserInGrid()).Returns("");

            _controller.RemoveUser();
            int UsersCount = _users.Count;

            Assert.That(UsersCount, Is.EqualTo(1));
            _users.Clear();
        }
        [Test]
        public void CheckIDInArray_UserFound_ReturnTrue()
        {
            _users.Add(new User("a", "a", "1", "1", User.SexOfPerson.Male));
            _controller.SelectedUserChanged("1");

            Assert.That(() => _controller.CheckIDInArray(), Is.False);
            _users.Clear();
        }
        [Test]
        public void CheckIDInArray_UserNotFound_ReturnFalse()
        {
            _users.Add(new User("a", "a", "1", "1", User.SexOfPerson.Male));

            _controller.SelectedUserChanged("1");

            Assert.That(() => _controller.CheckIDInArray(), Is.False);
            _users.Clear();
        }
    }
}