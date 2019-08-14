using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RewardProvider
/// </summary>
public class RewardProvider
{

    private String CompanyName;
    private String PhoneNumber;
    private String Email;
    private String Balance;
    private DateTime lastUpdated;
    private String LastUpdatedBy = "Darren Woodward";
    private DateTime LastUpdated = DateTime.Today;



    public RewardProvider(String CompanyName, String PhoneNumber, String Email)
    {
        setCompanyName(CompanyName);
        setPhoneNumber(PhoneNumber);
        setEmail(Email);

    }

    public void setCompanyName(String CompanyName)
    {
        this.CompanyName = CompanyName;
    }

    public void setPhoneNumber(String PhoneNumber)
    {
        this.PhoneNumber = PhoneNumber;
    }

    public void setEmail(String Email)
    {
        this.Email = Email;
    }
    public void setBalance(String Balance)
    {
        this.Balance = Balance;
    }

    public void setLastUpdatedBy(String LastUpdatedBy)
    {
        this.LastUpdatedBy = LastUpdatedBy;
    }

    public void setLastUpdated(DateTime LastUpdated)
    {
        this.LastUpdated = LastUpdated;
    }

    public string getCompanyName()
    {
        return this.CompanyName.ToString();
    }

    public string getPhoneNumber()
    {
        return this.PhoneNumber.ToString();
    }

    public string getBalance()
    {
        return this.Balance.ToString();
    }

    public string getEmail()
    {
        return this.Email;
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