using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GiftCard
/// </summary>
public class GiftCard
{
    private string rewardProviderID;
    private string value;
    private string notes;
    private DateTime datePosted;
    private DateTime dateRemoved;
    private string VoucherCode;
    private DateTime lastUpdated = DateTime.Today;
    private string lastUpdatedBy;
    
    public GiftCard(string rewardProviderID, string value, string notes, DateTime datePosted, DateTime dateRemoved, DateTime lasUpdated, string lastUpdatedBy)
    {
        setRewardProviderID(rewardProviderID);
        setValue(value);
        setNotes(notes);
        setDatePosted(datePosted);
        setDateRemoved(dateRemoved);
        setLastUpdatedBy(lastUpdatedBy);
    }

    public GiftCard(string value, string notes)
    {
         setValue(value);
        setNotes(notes);
        
    }

   

    public void setRewardProviderID(string rewardProviderID)
    {
        this.rewardProviderID = rewardProviderID;
    }

    public void setValue(string value)
    {
        this.value = value;
    }

    public void setNotes(string notes)
    {
        this.notes = notes;
    }

    public void setDatePosted(DateTime datePosted)
    {
        this.datePosted = datePosted;
    }

    public void setDateRemoved(DateTime dateRemoved)
    {
        this.dateRemoved = dateRemoved;
    }

    public void setLastUpdated(DateTime lastUpdated)
    {
        this.lastUpdated = lastUpdated;
    }

    public void setLastUpdatedBy(string lastUpdatedBy)
    {
        this.lastUpdatedBy = lastUpdatedBy;
    }

    public void setVoucherCode(string VoucherCode)
    {
        this.VoucherCode = VoucherCode;
    }

    public string getRewardProviderID()
    {
        return this.rewardProviderID;
    }

    public string getValue()
    {
        return this.value;
    }

    public string getNotes()
    {
        return this.notes;
    }

    public DateTime getDatePosted()
    {
        return this.datePosted;
    }

    public DateTime getDateRemoved()
    {
        return this.dateRemoved;
    }

    public string getVoucherCode()
    {
        return this.VoucherCode;
    }

    public String getLastUpdated()
    {
        return this.lastUpdated.ToShortDateString();
    }

    public string getLastUpdatedBy()
    {
        return this.lastUpdatedBy;
    }
}