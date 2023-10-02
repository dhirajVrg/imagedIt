<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editor.aspx.cs" Inherits="editor" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Editor</title>


<link type="text/css" href="css/home.css" rel="stylesheet"  media="screen" />
<link rel="stylesheet" type="text/css" href="css/styl.css" media="screen"/>
<link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />




<script type="text/javascript" src="js/jquery1.js"></script>
<script type='text/javascript' src='js/jquery-1.7.1.js'></script>
<!--<script src="js/Jquery-1.4.2.js" type="text/javascript"></script>-->
<!--<script src="js/jquery-1.10.2.min.js" type='text/javascript'></script>-->
<script type='text/javascript' src='pixastic-lib/pixastic.jquery.js'></script>
<script type='text/javascript' src='pixastic-lib/pixastic.core.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/lighten.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/blur.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/glow.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/hsl.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/edges.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/emboss.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/brightness.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/coloradjust.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/desaturate.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/noise.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/posterize.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/pointillize.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/mosaic.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/solarize.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/sharpen.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/invert.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/laplace.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/sepia.js'></script>
<script type='text/javascript' src='pixastic-lib/actions/unsharpmask.js'></script>
<script type="text/javascript">

    $(window).load(function () {
        var exts = document.getElementById("myImg").src;
        exts = exts.substring(exts.lastIndexOf(".") + 1);

        $("input#desaturate").click(function () {
            $("#myImg").pixastic("desaturate", { average: $("#check1").attr("checked") });
        });

        $("input#blur").click(function () {
            $("#myImg").pixastic("blur", { average: false });
        });

        $("input#brightness").click(function () {
            $("#myImg").pixastic("brightness", { brightness: $("#bright").val(), contrast: $("#cont").val(), legacy: $("#checklegacy").attr("checked") });
        });

        $("input#coloradjust").click(function () {
            $("#myImg").pixastic("coloradjust", { red: $("#RedColor").val(), green: $("#GreenColor").val(), blue: $("#BlueColor").val() });
        });

        $("input#edges").click(function () {
            $("#myImg").pixastic("edges", { mono: $("#checkmono").attr("checked"), invert: $("#checkinvert").attr("checked") });
        });

        $("input#emboss").click(function () {
            $("#myImg").pixastic("emboss", { blend: $("#emb").val(), direction: "topright", blend: false });
        });

        $("input#glow").click(function () {
            $("#myImg").pixastic("glow", { amount: $("#gl").val(), radius: $("#rad").val() });
        });

        $("input#hsl").click(function () {
            $("#myImg").pixastic("hsl", { hue: $("#h").val(), saturation: $("#sat").val(), lightness: $("#lig").val() });
        });

        $("input#lighten").click(function () {
            $("#myImg").pixastic("lighten", { amount: $("#light").val() });
        });

        $("input#noise").click(function () {
            $("#myImg").pixastic("noise", { amount: $("#no").val() }, { strength: 0.50 }, { mono: false });
        });

        $("input#poster").click(function () {
            $("#myImg").pixastic("posterize", { levels: $("#post").val() });
        });

        $("input#pointillize").click(function () {
            $("#myImg").pixastic("pointillize", { radius: 15, density: 1.5, noise: 0, transparent: false });
        });

        $("input#mosaic").click(function () {
            $("#myImg").pixastic("mosaic", { blockSize: $("#mos").val() });
        });

        $("input#solarize").click(function () {
            $("#myImg").pixastic("solarize", { blockSize: 8 });
        });

        $("input#sharpen").click(function () {
            $("#myImg").pixastic("sharpen", { amount: $("#sharp").val() });
        });

        $("input#invert").click(function () {
            $("#myImg").pixastic("invert");
        });

        $("input#laplace").click(function () {
            $("#myImg").pixastic("laplace", { grey: $("#led").val() });
        });

        $("input#sepia").click(function () {
            $("#myImg").pixastic("sepia");
        });

        $("input#unsharp").click(function () {
            $("#myImg").pixastic("unsharpmask", { amount: 500, radius: 2.00, threshold: 15 });
        });

        $("input#reset").click(function () {
            Pixastic.revert(document.getElementById("myImg"));
        });



        $(document).ready(function () {

            $("#save").click(function () {
                var image = document.getElementById("myImg");
                if (exts == "jpg" || exts == "jpeg") {
                    image = image.toDataURL("image/jpeg");
                    image = image.replace('data:image/jpeg;base64,', '');
                }
                else if (exts == "png") {
                    image = image.toDataURL("image/png");
                    image = image.replace('data:image/png;base64,', '');
                }
                else if (exts == "bmp") {
                    image = image.toDataURL("image/bmp");
                    image = image.replace('data:image/bmp;base64,', '');
                }
                //alert(image);

                //var datatobesent = String(image);
                //alert(datatobesent.length);
                //datatobesent = datatobesent.substring(1, 30);
                //alert(datatobesent);
                //var x = JSON.stringify({ imageData: datatobesent, exts: "exts" });
                $.ajax({
                    type: 'POST',
                    data: JSON.stringify({ imageData: image, exts: exts }), //'{ "imageData" : "' + image + '" }',
                    //data: {imageData: "image", exts:"exts"},
                    url: 'save.aspx/UploadImage',
                    cache: false,
                    //data: x,
                    contentType: 'application/json; charset=utf-8',
                    //dataType: 'json',
                    success: function (msg) {
                        //msg = JSON.stringify(msg);
                        alert("Image saved successfully!!");
                    },
                    error: function () {
                        alert("Error!!");

                    }


                });

            });

        });
    });

   </script>
 <script type="text/javascript">
     /*jQuery time*/
     $(document).ready(function () {
         $("#accordian h3").click(function () {
             //slide up all the link lists
             $("#accordian ul ul").slideUp();
             //slide down the link list below the h3 clicked - only if its closed
             if (!$(this).next().is(":visible")) {
                 $(this).next().slideDown();
             }
         })
     })
</script>
</head>
<body>
<div id="container">
<!--#include file="Header.aspx"-->


<form id="editor_form" runat="server">
<div id="accordian" style="float: left">
	<ul>
		<li>
			<h3>Desaturate</h3>    
            <ul>
             Use average:<input type="checkbox" id="check1" name="option1" value="Average" /> 
             <br />
            <asp:Button runat="server" class="button_example" id="desaturate" Text="Desaturate" OnClientClick="return false"/>
            </ul>
		</li>
		<!-- we will keep this LI open by default -->
		<li>
			<h3>Blur</h3>
			<ul>
				<asp:Button runat="server" class="button_example" id="blur" Text="Blur" OnClientClick="return false"/>
			</ul>
		</li>
		<li>
			<h3>Brightness/Contrast</h3>
			<ul>
                <li>
                <h6>Brightness</h6>
				<input type="range" id="bright" min="-150" max="150" value="0" step="3" onchange="showValue1(this.value)" />
                <span id="range1">0</span>
                
                <script type="text/javascript">
                            function showValue1(newValue) {
                            document.getElementById("range1").innerHTML = newValue;
                                                           }
                </script>		
                </li>
                <li>
                <h6>Contrast</h6>
                <input type="range" id="cont" min="-1" max="3" value="0" step="0.1" onchange="showValueContra(this.value)" />
                <span id="Contra">0</span>
                <script type="text/javascript">
                    function showValueContra(newValue) {
                        document.getElementById("Contra").innerHTML = newValue;
                    }
                </script>	
                Legacy:<input type="checkbox" id="checklegacy" name="option1" value="Legacy" /> 
                </li>
                <asp:Button runat="server" class="button_example" id="brightness" Text="Adjust B&C" OnClientClick="return false"/>
			</ul>
		</li>

        <li>
			<h3>Color Adjust</h3>
			<ul>
                <li>
                <h6>Red</h6>
				<input type="range" id="RedColor" min="-1" max="1" value="0" step="0.02" onchange="showValueRed(this.value)" />
                <span id="Red">0</span>
                
                <script type="text/javascript">
                                function showValueRed(newValue) {
                                document.getElementById("Red").innerHTML = newValue;
                                                               }
                </script>		
                </li>

                
                <li>
                <h6>Green</h6>
                <input type="range" id="GreenColor" min="-1" max="1" value="0" step="0.02" onchange="showValueGreen(this.value)" />
                <span id="Green">0</span>
                
                <script type="text/javascript">
                    function showValueGreen(newValue) {
                        document.getElementById("Green").innerHTML = newValue;
                    }
                </script>
                </li>


                <li>
                <h6>Blue</h6>
                <input type="range" id="BlueColor" min="-1" max="1" value="0" step="0.02" onchange="showValueBlue(this.value)" />
                <span id="Blue">0</span>
                
                <script type="text/javascript">
                    function showValueBlue(newValue) {
                        document.getElementById("Blue").innerHTML = newValue;
                    }
                </script>
                </li>


                <asp:Button runat="server" class="button_example" id="coloradjust" Text="Color Adjust" OnClientClick="return false"/>
			</ul>
		</li>



		<li>
			<h3>Edges</h3>
			<ul>
                 Mono:<input type="checkbox" id="checkmono" name="option1" value="Mono" /> <br />
                 Invert:<input type="checkbox" id="checkinvert" name="option1" value="Invert" /> <br />
				<asp:Button runat="server" class="button_example" id="edges" Text="Edges" OnClientClick="return false"/>
			</ul>
		</li>


		<li>
			<h3>Emboss</h3>
			<ul>
				<input type="range" id="emb" min="0" max="10" value="0" step="0.1" onchange="showValue2(this.value)" />
                <span id="range2">0</span>
                <asp:Button runat="server" class="button_example" id="emboss" Text="Emboss" OnClientClick="return false"/>
                <script type="text/javascript">
                            function showValue2(newValue) {
                            document.getElementById("range2").innerHTML = newValue;
                                                            }
                </script>		
			</ul>
		</li>


		<li>
			<h3>Glow</h3>
			<ul>
            <li>
            <h6>Glow</h6>
				<input type="range" id="gl" min="0.00" max="1" value="0" step="0.01" onchange="showValue3(this.value)" />
                <span id="range3">0</span>
                <script type="text/javascript">
                              function showValue3(newValue) {
                              document.getElementById("range3").innerHTML = newValue;
                                                            }
                </script>
            </li>


            
                <asp:Button runat="server" class="button_example" id="glow" Text="Glow" OnClientClick="return false" />		
			</ul>
		</li>


		<li>
			<h3>HSL</h3>
			<ul>
				<li>
                    <h6>Hue</h6>
                    <input type="range" id="h" min="-180" max="180" value="0" step="3" onchange="showValueh(this.value)" />
                    <span id="rangeh">0</span>
                    <script type="text/javascript">
                            function showValueh(newValue) {
                            document.getElementById("rangeh").innerHTML = newValue;
                                        }
                    </script>		
                </li>

                <li>
                <h6>Saturation</h6>
                    <input type="range" id="sat" min="-100" max="100" value="0" step="2" onchange="showValues(this.value)" />
                    <span id="ranges">0</span>
                    <script type="text/javascript">
                            function showValues(newValue) {
                            document.getElementById("ranges").innerHTML = newValue;
                               }
                    </script>
                 </li>

                 <li>
                  <h6>Lighten</h6>
                            <input type="range" id="lig" min="-100" max="100" value="0" step="2" onchange="showValuel(this.value)" />
                            <span id="rangel">0</span>
                            <script type="text/javascript">
                                    function showValuel(newValue) {
                                    document.getElementById("rangel").innerHTML = newValue;
                                            }
                            </script>		
                  </li>
                  <asp:Button runat="server" class="button_example" id="hsl" Text="HSL" OnClientClick="return false"/>	
			</ul>
		</li>


		<li>
			<h3>Lighten</h3>
			<ul>
				<input type="range" id="light" min="-1" max="1" value="0" step="0.1" onchange="showValue4(this.value)" />
                <span id="range4">0</span>
                <asp:Button runat="server" class="button_example" id="lighten" Text="Lighten" OnClientClick="return false"/>
                <script type="text/javascript">
                            function showValue4(newValue) {
                            document.getElementById("range4").innerHTML = newValue;
                                                            }
                </script>		
			</ul>
		</li>



		<li>
			<h3>Posterize</h3>
			<ul>
				<input type="range" id="post" min="1" max="32" value="0" step="1" onchange="showValue6(this.value)" />
                <span id="range6">0</span>
                <asp:Button runat="server" class="button_example" id="poster" Text="Posterize" OnClientClick="return false"/>
                <script type="text/javascript">
                            function showValue6(newValue) {
                            document.getElementById("range6").innerHTML = newValue;
                                                            }
                </script>		
			</ul>
		</li>



		<li>
			<h3>Mosaic</h3>
			<ul>
				<input type="range" id="mos" min="1" max="100" value="0" step="1" onchange="showValue7(this.value)" />
                <span id="range7">0</span>
                <asp:Button runat="server" class="button_example" id="mosaic" Text="Mosaic" OnClientClick="return false" />
                <script type="text/javascript">
                            function showValue7(newValue) {
                            document.getElementById("range7").innerHTML = newValue;
                                                            }
                </script>		
			</ul>
		</li>


		<li>
			<h3>Sharpen</h3>
			<ul>
				<input type="range" id="sharp" min="0.00" max="1" value="0" step="0.001" onchange="showValue8(this.value)" />
                <span id="range8">0</span>
                <asp:Button runat="server" class="button_example" id="sharpen" Text="Sharpen" OnClientClick="return false"/>
                <script type="text/javascript">
                                function showValue8(newValue) {
                                document.getElementById("range8").innerHTML = newValue;
                                                              }
                </script>		
			</ul>
		</li>


		<li>
			<h3>Solarize</h3>
			<ul>
				<asp:Button runat="server" class="button_example" id="solarize" Text="Solarize" OnClientClick="return false"/>
			</ul>
		</li>


		<li>
			<h3>Invert</h3>
			<ul>
				<asp:Button runat="server" class="button_example" id="invert" Text="Invert" OnClientClick="return false"/>
			</ul>
		</li>


		<li>
			<h3>Laplace Edge Detection</h3>
			<ul>
				<input type="range" id="led" min="0" max="255" value="0" step="3" onchange="showValue9(this.value)" />
                <span id="range9">0</span>
                <asp:Button runat="server" class="button_example" id="laplace" Text="Laplace edge detection" OnClientClick="return false"/>
                <script type="text/javascript">
                                function showValue9(newValue) {
                                document.getElementById("range9").innerHTML = newValue;
                                                }
                </script>		
			</ul>
		</li>

	</ul>



</div>

    <img id="myImg" src="<%= Getlocation()%>" style="float:left; max-height:700px; max-width:700px; margin:10px 10px 10px 50px;"/><br/>
    <asp:Button ID="save" runat="server" class="button_example" Text="Save" OnClientClick="return false" />
<asp:Button ID="reset" runat="server" class="button_example" Text="Reset" OnClientClick="return false" /><br/>
 </form>    


  

</div>

</body>
</html>