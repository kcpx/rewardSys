using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class MoneyTransaction
{
    double TotalAmount;
    double TransactionAmount;
    string Date;
    String LastUpdated;
    String LastUpdatedBy;
    int PersonID;
    //array 
    public static MoneyTransaction[] moneyTransactionArray = new MoneyTransaction[5];
    public static int i = 0;
    public MoneyTransaction(double TotalAmount, string Date, double TransactionAmount, String LastUpdated, String LastUpdatedBy,int PersonID)
    {
        setPoints(TotalAmount);
        setDate(Date);
        setDescription(TransactionAmount);
        setLUD(LastUpdated);
        setLUDB(LastUpdatedBy);
        setPersonID(PersonID);
    }
    //getters and setters to array
    public void addToArray(MoneyTransaction newTransaction)
    {
        moneyTransactionArray[i] = this;
        i++;
    }
    //public static double getTransaction(int o)
    //{
        //return Double.Parse(this.moneyTransactionArray[o].getTransactionAmount());
    //}
    //
    public void setPoints(double TotalAmount)
    {
        this.TotalAmount = TotalAmount;
    }

    public void setDate(string Date)
    {
        this.Date = Date;
    }

    public void setDescription(double TransactionAmount)
    {
        this.TransactionAmount = 0-TransactionAmount;
    }

    public void setLUD(string LastUpdated)
    {
        this.LastUpdated = LastUpdated;
    }

    public void setLUDB(string LastUpdatedBy)
    {
        this.LastUpdatedBy = LastUpdatedBy;
    }

    public void setPersonID(int PersonID)
    {
        this.PersonID = PersonID;
    }
    public double getPoints()
    {
        return this.TotalAmount;
    }

    public string getDate()
    {
        return this.Date;
    }

    public string getTransactionAmount()
    {

        return this.TransactionAmount.ToString();
    }

    public string getLUD()
    {
        return this.LastUpdated;
    }

    public string getLUDB()
    {
        return this.LastUpdatedBy;
    }

    public int getPersonID()
    {
        return this.PersonID;
    }
}

