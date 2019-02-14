using Shared.Kernel.ForDomain;

namespace Application.Dto._4Digests
{
    public class HouseDto
    {
        public long Id { get; set; }

        public AddressDto Address { get; set; }
        public int? Year { get; set; }
        public string MetroStation { get; set; }

        public WallMaterialDto WallMaterial { get; set; }
    }
}