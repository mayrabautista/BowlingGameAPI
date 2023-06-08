using BowlingGame.Core.Aplication.Validators;
using BowlingGame.Core.Domain.Models;
using FluentValidation.TestHelper;

namespace BowlingGame.Core.Validators.Tests
{
    public class FramesValidatorTests
    {
        private FrameValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new FrameValidator();
        }

        [Test]
        public void ShouldHaveValidationWhenSumOverTen()
        {
            var model = new Frame { 
            FirstRoll = 1,
            SecondRoll = 12,
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.FirstRoll);
        }

        [Test]
        public void ShouldHaveValidationWhenIsSprikeAndFirstRollIsUnderTen()
        {
            var model = new Frame
            {
                FirstRoll = 1,
                IsStrike = true
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.IsStrike);
        }

        [Test]
        public void ShouldHaveValidationWhenIsSpareAndSumUpIsUnderTen()
        {
            var model = new Frame
            {
                FirstRoll = 1,
                IsSpare = true
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.IsSpare);
        }
    }
}
