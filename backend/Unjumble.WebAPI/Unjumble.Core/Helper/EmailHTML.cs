using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Helper
{
    public static class EmailHTML
    {
        public static string ReturnEmailActivationHTML(string activationCode, string fullName)
        {
            return @"<table width='100%' cellpadding='20%'>
                        <tr>
                          <td align='center'>
                            <table padding:='10%' width='50%' cellspacing='0' cellpadding='0'
                              style='color: #999999; background-color: #F7F7F7; border-radius: 5px; overflow: hidden;'>
                              <tbody>
                                <tr>
                                  <td width='520' valign='top' align='center'>
                                    <table width='100%' cellspacing='0' cellpadding='0'>
                                      <tbody>
                                        <tr>
                                          <td>
                                            <img
                                              src='https://res.cloudinary.com/celik/image/upload/v1605911946/bannerTop_os3al7.png'
                                              alt='' width='100%' height='auto'>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align='center'><img
                                              src='https://res.cloudinary.com/celik/image/upload/v1605911870/logo_cuf019.png'
                                              alt='' width='100px' height='100px' style='margin:5%'></td>
                                        </tr>
                                        <tr>
                                          <td align='center'>
                                            <h2 style='margin-left: 5%; margin-right: 5%;'>Welcome<span style='font-weight: bold;'>
                                                " + fullName + @",</span></h2>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align='center'>
                                            <p style='margin-left: 5%; margin-right: 5%;'>Thank you for registering to our app! Your
                                              activation code is:</p>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align='center'>
                                            <h1 style='margin-left: 5%; margin-right: 5%;'>" + activationCode + @"</h1>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align='center'>
                                            <p
                                              style=' border-top: 1px solid #a3a3a3b2; padding-top: 2em; margin-left: 5%; margin-right: 5%;'>
                                              You can also activate by
                                              clicking
                                              <a href='http://localhost:4200/signup/" + activationCode + @"'>here</a></p>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td>
                                            <img
                                              src='https://res.cloudinary.com/celik/image/upload/v1605911974/bannerBottom_a1r3a2.png'
                                              alt='' width='100%' height='auto'>
                                          </td>
                                        </tr>
                                      </tbody>
                                    </table>
                                  </td>
                                </tr>
                              </tbody>
                            </table>
                          </td>
                        </tr>
                      </table>";
        }
    }
}
