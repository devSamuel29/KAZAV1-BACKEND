using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace kazariobranco_backend.Models;

public class ContactModel
{
    public int id { get; set; }
   
    public string name { get; set; }

    public string email { get; set; }

    public string phone { get; set; }

    public string reason { get; set; }

    public string description { get; set; }

    public DateTime created_at { get; set; }

    public bool ended { get; set; }
}
