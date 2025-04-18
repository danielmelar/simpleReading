using Microsoft.EntityFrameworkCore.Migrations.Operations;
using simpleReading.Models;

namespace simpleReading.ViewModel
{
    public class UpdateViewModel
    {
        public UpdateViewModel()
        { }

        public UpdateViewModel(Read? read, GroupedReadsViewModel? groupedReads)
        {
            Read = read;
            GroupedReads = groupedReads;
        }
        public Read? Read { get; set; }
        public GroupedReadsViewModel? GroupedReads { get; set; }
    }
}
