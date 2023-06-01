using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kazariobranco_backend.Models;

public class AddressModel
{
    public int Id { get; set; }

    public string Address { get; set; }

    public int Number { get; set; }

    public string District { get; set; }

    public string State { get; set; }

    public string City { get; set; }
    public int ZipCode { get; set; }
}
