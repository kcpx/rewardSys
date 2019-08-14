using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Company
/// </summary>
public class Company
{
    private String BusinessEntityName;
    private String Email;
    private String PhoneNumber;
    private String LastUpdatedBy = "Darren Woodward";
    private DateTime LastUpdated = DateTime.Today;

    public Company(string BusinessEntityName, string PhoneNumber, string Email)
    {
        setBusinessEntityName(BusinessEntityName);
        setPhoneNumber(PhoneNumber);
        setEmail(Email);
    }

    public void setBusinessEntityName(String BusinessEntityName)
    {
        this.BusinessEntityName = BusinessEntityName;
    }

    public void setPhoneNumber(String PhoneNumber)
    {
        this.PhoneNumber = PhoneNumber;
    }

    public void setEmail(String Email)
    {
        this.Email = Email;
    }
    public void setLastUpdatedBy(String LastUpdatedBy)
    {
        this.LastUpdatedBy = LastUpdatedBy;
    }

    public void setLastUpdated(DateTime LastUpdated)
    {
        this.LastUpdated = LastUpdated;
    }

    public string getEmail()
    {
        return this.Email;
    }

    public string getBusinessEntityName()
    {
        return this.BusinessEntityName;
    }

    public string getPhoneNumber()
    {
        return this.PhoneNumber;
    }

    public String getLastUpdatedBy()
    {
        return this.LastUpdatedBy;
    }

    public String getLastUpdated()
    {
        return this.LastUpdated.ToShortDateString();
    }
}