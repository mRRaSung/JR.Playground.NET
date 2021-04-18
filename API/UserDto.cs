using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }

        [StringLength(maximumLength: 12, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
