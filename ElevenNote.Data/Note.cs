using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Your Note")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Content { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; } // DateTimeOffset is a value type, they can't be null
        public DateTimeOffset? ModifiedUtc { get; set; } // Adding the ? allows a value type to be null.  It's good to store dates
                                                         // with DateTimeOffset, this way it will account for time zones.  ? = null-conditional operator.
    }
}
// Objects are reference types, they can be null.
// Reference Types point to an address in memory.
