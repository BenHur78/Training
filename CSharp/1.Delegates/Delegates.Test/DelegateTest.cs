using FluentAssertions;

namespace Delegates.Test
{
    public class DelegateTest
    {
        [Fact]
        public void Event_ShouldBe_Null()
        {
            //Arrange
            var sut = new DelegateFixture();

            //Act
            var result = sut.RaiseSimpleEvent();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Event_ShouldBe_NotNull()
        {
            //Arrange
            var sut = new DelegateFixture();

            //Act
            sut.Subscribe();
            int numberOfSubscribers = sut.GetNumberOfSubscribers();
            var result = sut.RaiseSimpleEvent();

            //Assert
            result.Should().BeTrue();
            numberOfSubscribers.Should().Be(1);
        }

        [Fact]
        public void Event_ShouldHave_OnlyOneSubscriber()
        {
            //Arrange
            var sut = new DelegateFixture();

            //Act
            sut.Subscribe();
            int numberOfSubscribers = sut.GetNumberOfSubscribers();

            //Assert
            numberOfSubscribers.Should().Be(1);
        }

        /// <summary>
        /// From this unit test, I have learned that when you unsubscribe,
        ///  the event becomes null.
        /// </summary>
        [Fact]
        public void Event_ShouldNotHave_Subscribers()
        {
            //Arrange
            var sut = new DelegateFixture();

            //Act
            sut.Subscribe();
            sut.Unsubscribe();
            int numberOfSubscribers = sut.GetNumberOfSubscribers();

            //Assert
            numberOfSubscribers.Should().Be(0);
        }

        [Fact]
        public void WhenSubscribingTwoTimesToTheEventWithSameHandler_EventInvocationList_ShouldHave_TwoSubscribers()
        {
            //Arrange
            var sut = new DelegateFixture();

            //Act
            sut.Subscribe();
            sut.Subscribe();
            int numberOfSubscribers = sut.GetNumberOfSubscribers();
            sut.RaiseSimpleEvent();

            //Assert
            numberOfSubscribers.Should().Be(2);
            sut.Count.Should().Be(2);
        }

        /// <summary>
        /// This is an integration test where we subscribe and unsubscribe multiple times.
        /// </summary>
        [Fact]
        public void AScenarioWhereWeSubscribe_AndUnsubscribeMultipleTimes()
        {
            //Arrange
            var sut = new DelegateFixture();

            //Act
            sut.Subscribe();
            sut.SubscribeWithDifferentHandler();
            sut.Subscribe();
            int numberOfSubscribers = sut.GetNumberOfSubscribers();
            sut.RaiseSimpleEvent();

            //Assert
            numberOfSubscribers.Should().Be(3);
            sut.Count.Should().Be(2);
            sut.CountDifferentHandler.Should().Be(1);

            //Act
            sut.Unsubscribe();
            numberOfSubscribers = sut.GetNumberOfSubscribers();

            //Assert
            numberOfSubscribers.Should().Be(2);

            //Act
            sut.UnsubscribeWithDifferentHandler();
            numberOfSubscribers = sut.GetNumberOfSubscribers();
            
            //Assert
            numberOfSubscribers.Should().Be(1);

            //Act
            sut.Unsubscribe();
            numberOfSubscribers = sut.GetNumberOfSubscribers();

            //Assert
            numberOfSubscribers.Should().Be(0);
        }

        private class DelegateFixture
        {
            public event EventHandler SimpleEvent;

            public int Count { get; private set; } = 0;
            public int CountDifferentHandler { get; private set; } = 0;

            public int GetNumberOfSubscribers()
            {
                if(SimpleEvent == null)
                {
                    return 0;
                }

                return SimpleEvent.GetInvocationList().Length;
            }

            public void Subscribe()
            {
                SimpleEvent += HandleSimpleEvent;
            }

            public void SubscribeWithDifferentHandler()
            {
                SimpleEvent += DifferentHandler_HandleSimpleEvent;
            }

            public void Unsubscribe()
            {
                SimpleEvent -= HandleSimpleEvent;
            }

            public void UnsubscribeWithDifferentHandler()
            {
                SimpleEvent -= DifferentHandler_HandleSimpleEvent;
            }

            public bool RaiseSimpleEvent()
            {
                SimpleEvent?.Invoke(this, EventArgs.Empty);

                if (SimpleEvent != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            private void HandleSimpleEvent(object sender, EventArgs e)
            {
                Count++;
            }

            private void DifferentHandler_HandleSimpleEvent(object sender, EventArgs e)
            {
                CountDifferentHandler++;
            }
        }
    }
}