using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ctlPager : System.Web.UI.UserControl
{
    protected string _recordMessage = "<b>{0}</b> - <b>{1}</b> of <b>{2}</b>";
    private int _totalRecordsCount = 1;
    private int _pageSize = 50;
    private string _preFix = "";
    private int _pagesToDisplay = 10;
    private int TempVar;
    private double TempVarDouble;
    /// <summary>
    /// Occurs when [page index change].
    /// </summary>
    /// <remarks></remarks>
    public event EventHandler<PagerCommandEventArgs> PageIndexChange;

    /// <summary>
    /// Set the PagesToDisplay.
    /// </summary>
    /// <remarks></remarks>
    public int PagesToDisplay
    {
        set
        {
            if (value > 1)
            {
                _pagesToDisplay = value;
            }
            else
            {
                throw new Exception("Pages to display should be greater than 1");
            }
        }
    }

    /// <summary>
    /// Set the Prefix.
    /// </summary>
    /// <remarks></remarks>
    public string Prefix
    {
        set { _preFix = value; }
    }

    /// <summary>
    /// Set the Page Size.
    /// </summary>
    /// <remarks></remarks>
    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            if (value > 4 && value < 201)
            {
                _pageSize = value;
            }
            else
            {
                throw new Exception("Page Size should be between 5 and 200");
            }
        }
    }

    /// <summary>
    /// Set the Total Records Count.
    /// </summary>
    /// <remarks></remarks>  
    public int TotalRecordsCount
    {
        set
        {
            if (value >= 0)
            {
                _totalRecordsCount = value;
                if (_totalRecordsCount <= 0)
                {
                    this.Visible = false;
                }
                else
                {
                    bindRepetor();
                    this.Visible = true;
                }
            }
            else
            {
                throw new Exception("TotalRecordsCount greater then equal to 0");
            }
        }
    }

    /// <summary>
    /// Set the Current Row Index.
    /// </summary>
    /// <remarks></remarks>
    public int CurrentRowIndex
    {
        get { return ((this.CurrentPageNumber - 1) * _pageSize) + 1; }
    }

    /// <summary>
    /// Get or Set the Current Page Number.
    /// </summary>
    /// <remarks></remarks> 
    public int CurrentPageNumber
    {
        get
        {
            if (ViewState["cPage"] == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(ViewState["cPage"]);
            }
        }

        set
        {
            if (value > 0)
            {
                ViewState["cPage"] = value;
                bindRepetor();
            }
            else
            {
                throw new Exception("CurrentPageNumber should be greater then 0");
            }
        }
    }    

    

    /// <summary>
    /// Handles the PageIndexChange event of the Pager control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="MyycoHS.PagerCommandEventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    private void Pager_PageIndexChange(object sender, System.EventArgs e)
    {
        OnPageIndexChanged(new PagerCommandEventArgs());
    }

    /// <summary>
    /// Handles the OnPageIndexChanged event .
    /// </summary>
    /// <param name="e">The <see cref="MyycoHS.PagerCommandEventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void OnPageIndexChanged(PagerCommandEventArgs e)
    {
        if (PageIndexChange != null)
        {
            PageIndexChange(this, e);
        }
    }

    /// <summary>
    /// Handles the Init event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>

    private void Page_Init(object sender, System.EventArgs e)
    {
        PageSize = 10; //Deault to 10. 
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        lblPrefix.Text = _preFix;
    }

    /// <summary>
    /// Handles the ItemCommand event of the Rptpager control.
    /// </summary>
    /// <param name="source">The source of the event</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Rptpager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int intArgs = int.Parse(e.CommandArgument.ToString());
        this.CurrentPageNumber = intArgs;
        Pager_PageIndexChange(this, e);
    }

    /// <summary>
    /// Handles the Command event of the lnkPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void lnkPrevious_Command(object sender, CommandEventArgs e)
    {
        this.CurrentPageNumber = this.CurrentPageNumber - 1;
        Pager_PageIndexChange(this, e);
    }

    /// <summary>
    /// Handles the Command event of the lnkNext control.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void lnkNext_Command(object sender, CommandEventArgs e)
    {
        this.CurrentPageNumber = this.CurrentPageNumber + 1;
        Pager_PageIndexChange(this, e);
    }

    /// <summary>
    /// Handles the Command event of the lnkFirst control.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void lnkFirst_Command(object sender, CommandEventArgs e)
    {
        this.CurrentPageNumber = 1;
        Pager_PageIndexChange(this, e);
    }

    /// <summary>
    /// Handles the Command event of the lnkLast control.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void lnkLast_Command(object sender, CommandEventArgs e)
    {
        this.CurrentPageNumber = int.Parse(e.CommandArgument.ToString());
        Pager_PageIndexChange(this, e);
    }  

    /// <summary>
    /// bind the Repetor.
    /// </summary>
    /// <remarks></remarks>
    private void bindRepetor()
    {
        int start_pos = 0;
        int end_pos = 0;
        int halfSide = Convert.ToInt32((double.TryParse(Convert.ToString(_pagesToDisplay / 2), out TempVarDouble) ? TempVarDouble : 0));

        int m_PageCount = this.totalPages();
        int _currentPageNumber = this.CurrentPageNumber;

        lnkLast.CommandArgument = m_PageCount.ToString();
        if (_currentPageNumber > m_PageCount)
        {
            _currentPageNumber = m_PageCount;
        }
        else if (_currentPageNumber < 1)
        {
            _currentPageNumber = 1;
        }

        if (_currentPageNumber - halfSide <= 0)
        {
            start_pos = 1;
        }
        else
        {
            if (_pagesToDisplay % 2 == 0)
            {
                start_pos = _currentPageNumber - halfSide + 1;
            }
            else
            {
                start_pos = _currentPageNumber - halfSide;
            }
        }
        end_pos = _currentPageNumber + halfSide;

        if (end_pos > m_PageCount)
        {
            end_pos = m_PageCount;
            start_pos = end_pos - _pagesToDisplay + 1;
        }
        else if (end_pos < _pagesToDisplay)
        {
            end_pos = _pagesToDisplay;
            start_pos = 1;
        }

        if (end_pos > m_PageCount)
        {
            end_pos = m_PageCount;
        }
        if (start_pos <= 0)
        {
            start_pos = 1;
        }

        if (_currentPageNumber == 1)
        {
            lnkFirst.Visible = false;
            lnkPrevious.Visible = false;
        }
        else
        {
            lnkFirst.Visible = true;
            lnkPrevious.Visible = true;
        }


        if (_currentPageNumber == m_PageCount)
        {
            lnkNext.Visible = false;
            lnkLast.Visible = false;
        }
        else
        {
            lnkNext.Visible = true;
            lnkLast.Visible = true;
        }

        DataTable dtpager = new DataTable("Pager");
        dtpager.Columns.Add(new DataColumn("IntPageNo", typeof(int)));

        int inti = 0;
        DataRow dbrow = null;
        for (inti = start_pos; inti <= end_pos; inti++)
        {
            dbrow = dtpager.NewRow();
            dbrow[0] = inti;
            dtpager.Rows.Add(dbrow);
        }

        Rptpager.DataSource = dtpager;
        Rptpager.DataBind();
        if (_totalRecordsCount <= _pageSize)
        {
            Rptpager.Visible = false;
        }
        else
        {
            Rptpager.Visible = true;
        }
        int lastRecord = Convert.ToInt32(((this.CurrentRowIndex + _pageSize) > _totalRecordsCount ? _totalRecordsCount : (this.CurrentRowIndex + _pageSize - 1)));
        lblRecordMessage.Text = String.Format(_recordMessage, this.CurrentRowIndex.ToString(), lastRecord.ToString(), _totalRecordsCount.ToString());
    }

    /// <summary>
    /// return total Pages
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    private int totalPages()
    {
        int intrem = 0;
        int intPageno = System.Math.DivRem(_totalRecordsCount, _pageSize, out intrem);
        if (intrem > 0)
        {
            intPageno += 1;
        }
        return intPageno;
    }
    public ctlPager()
    {
        Load += Page_Load;
        Init += Page_Init;
    }

    
}