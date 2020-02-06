using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace UL.Tech.Test
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private IParser GetSelectedParser()
        {
            if (ddlParser.SelectedValue == "0")
            {
                return new OperatorPrecedenceParser();
            }
            else
            {
                return new RecursiveParser();
            }
        }

        protected void btnEvaluate_Click(object sender, EventArgs e)
        {
            lblResult.CssClass = lblResult.CssClass.Replace("error", "");
            if (!string.IsNullOrEmpty(txtInput.Text))
            {
                IParser parser = GetSelectedParser();
                try
                {
                    double result = parser.Parse(txtInput.Text);
                    lblResult.Text = result.ToString();
                }
                catch (ParserException ex)
                {
                    lblResult.Text = ex.Message;
                    lblResult.CssClass = "error";
                }
                
            }
            else
            {
                lblResult.Text = string.Empty;
            }
        }
    }
}