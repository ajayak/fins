using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.DTO;

namespace FINS.Features.Common.State
{
    public class StateDto : BaseDto<int>
    {
        /// <summary>
        /// Name of the state
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// State code
        /// </summary>
        public string Code { get; set; }
    }
}
