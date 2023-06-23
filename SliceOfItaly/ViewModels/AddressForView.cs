using SliceOfItalyAPI.Models.Abstract;
using SliceOfItalyAPI.Models;
using SliceOfItalyAPI.Helpers;

namespace SliceOfItalyAPI.ViewModels;

public class AddressForView : BaseDataTable
{
    public string AddressLine1 { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string City { get; set; } = default!;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public static explicit operator Address(AddressForView address)
    {
        var result = new Address().CopyProperties(address);
        return result;
    }
    public static implicit operator AddressForView(Address address)
    {
        var result = new AddressForView
        {
            CustomerId = address.CustomerId,
            CustomerName = address.Customer.Name,
            AddressLine1 = address.AddressLine1,
            PostalCode = address.PostalCode,
            City = address.City
        }.CopyProperties(address);
        return result;
    }
}