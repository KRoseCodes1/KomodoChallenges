using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using KomodoBadges_Repo;

namespace KomodoBadges_Tests
{
    [TestClass]
    public class BadgesRepoTests
    {
        private BadgeRepo _repo;
        private Badge badge;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new BadgeRepo();
            badge = new Badge();
        }
        [TestMethod]
        public void CreateNewBadge_ShouldReturnTrue()
        {
            // Arrange
            badge.BadgeID = 10222;
            // Act
            _repo.AddNewBadge(badge);
            Dictionary<int, List<string>> dictFromRepo = _repo.ViewAllBadges();
            bool contains = dictFromRepo.ContainsKey(10222);

            // Assert
            Assert.IsTrue(contains);
        }
        [TestMethod]
        public void UpdateExistingBadge_ShouldBeEqual()
        {
            // Arrange
            badge.DoorAccess = new List<string>() { "A1", "B2", "C3", "D4" };
            badge.BadgeID = 10223;
            _repo.AddNewBadge(badge);
            List<string> updatedDoors = new List<string>() { "A1", "B2", "C3" };

            // Act
            _repo.UpdateExistingBadge(10223, updatedDoors);
            Dictionary<int, List<string>> dictFromRepo = _repo.ViewAllBadges();

            // Assert
            Assert.AreEqual(updatedDoors, dictFromRepo[10223]);
        }
        [TestMethod]
        public void RemoveBadge_ShouldReturnFalse()
        {
            // Arrange
            badge.BadgeID = 10224;
            _repo.AddNewBadge(badge);

            // Act
            _repo.RemoveBadge(10224);
            Dictionary<int, List<string>> dictFromRepo = _repo.ViewAllBadges();
            bool contains = dictFromRepo.ContainsKey(10224);

            // Assert
            Assert.IsFalse(contains);
        }
    }
}

