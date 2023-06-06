using BowlingGame.Core.Aplication.Validators;
using BowlingGame.Core.Domain.Models;
using FluentValidation.TestHelper;

namespace BowlingGame.Core.Validators.Tests
{
    public class GameValidatorTests
    {
        private GameValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new GameValidator();
        }

        [Test]
        public void ShouldHaveErrorWhenNameIsNull()
        {
            var model = new Game {};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.PlayerName);
        }
    }
}