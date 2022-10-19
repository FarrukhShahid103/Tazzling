using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLNewsLetters
{
    #region Private Variables
    
    private long NlId;
    private string Title;
    private string NewsLetter;
    private DateTime CreatedDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;

    #endregion

    #region Constructor

    public BLLNewsLetters()
    {
        NlId = 0;
        Title = "";
        NewsLetter = "";
        CreatedDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
    }

    #endregion

    #region Properties

    public long nlId
    {
        set { NlId = value; }
        get { return NlId; }
    }

    public string title
    {
        set { Title = value; }
        get { return Title; }
    }

    public string newsLetter
    {
        set { NewsLetter = value; }
        get { return NewsLetter; }
    }

    public DateTime createdDate
    {
        set { CreatedDate = value; }
        get { return CreatedDate; }
    }

    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }
        
    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }
    
    #endregion

    #region Functions

    public DataTable getAllNewsLetter()
    {
        return DALNewsLetters.getAllNewsLetter();
    }

    public DataTable getNewsLetterInfoById()
    {
        return DALNewsLetters.getNewsLetterInfoById(this);
    }

    public int deleteNewsLetterById()
    {
        return DALNewsLetters.deleteNewsLetterById(this);
    }

    public int updateNewsLetterById()
    {
        return DALNewsLetters.updateNewsLetterById(this);
    }

    public int createNewsLetter()
    {
        return DALNewsLetters.createNewsLetter(this);
    }

    #endregion

}
