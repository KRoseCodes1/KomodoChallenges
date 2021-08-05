using KomodoClaims_Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KomodoClaims_Tests
{
    [TestClass]
    public class ClaimRepoTests
    {
        private ClaimRepo _repo;
        private Claim claim;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new ClaimRepo();
            claim = new Claim();
        }
        [TestMethod]
        public void AddNewClaim_ShouldReturnEqual()
        {
            // Act
            _repo.AddNewClaim(claim);
            Claim fromQueue = _repo._currentQueue.Peek();

            // Assert
            Assert.AreEqual(claim, fromQueue);
        }
        [TestMethod]
        public void RemoveFromQueue_ShouldReturnEqualToZero()
        {
            // Arrange
            _repo.AddNewClaim(claim);

            // Act
            _repo.RemoveClaim();
            int count = _repo._currentQueue.Count;

            // Assert
            Assert.AreEqual(0, count);
        }
    }
}