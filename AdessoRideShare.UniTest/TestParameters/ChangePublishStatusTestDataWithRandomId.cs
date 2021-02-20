using AdessoRideShare.Api.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdessoRideShare.UniTest.TestParameters
{
    public class ChangePublishStatusTestDataWithRandomId : TheoryData<ChangePublishStatusParameters>
    {
        public ChangePublishStatusTestDataWithRandomId()
        {
            Add(new ChangePublishStatusParameters
            {
                TestChangeStatusDto = new ChangeStatusDto { Id = "60312e57485f3bcd6fda8faa" }
            });
        }
    }
}
