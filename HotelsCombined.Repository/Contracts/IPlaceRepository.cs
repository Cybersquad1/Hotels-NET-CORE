using HotelsCombined.Entities;

namespace HotelsCombined.Repository.Contracts
{
	public interface IPlaceRepository
	{
		/// <summary>
		/// Gets a single place file file name, or null if not found.
		/// </summary>
		Place Get(string fileName);
	}
}