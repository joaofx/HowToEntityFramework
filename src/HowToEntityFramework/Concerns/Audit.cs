using System;
using HowToEntityFramework.Infra;

namespace HowToEntityFramework.Concerns
{
    public class Audit
    {
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void BeingCreated()
        {
            CreatedAt = App.Clock();
            UpdatedAt = App.Clock();
        }

        public void BeingUpdated()
        {
            UpdatedAt = App.Clock();
        }
    }
}