<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication6._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
        </div>
        <div id="queryDiv">
            <label>产品名称:</label>
            <input type="text" id="nameId" value="电脑"/>
            <label id="priceId">价格:</label>
            <input type="button" value="查询" onclick="queryByName()" />
        </div>
        <script type="text/javascript">
            function queryByName() {
                var o = $("#nameId").val();
                $.getJSON("Default.aspx",
                    { Name: o },
                    function (data) {
                        $("#priceId").text($("#priceId").text()+ data);
                    });
            }
        </script>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>下面是我们的建议:</h3>
    <ol class="round">
        <li class="one">
            <h5>开始使用</h5>
            通过 ASP.NET Web Forms，可以使用一种熟悉的、支持拖放操作的事件驱动模型生成动态网站。
            一个设计图面加上数百个控件和组件，使你能够快速生成复杂且功能强大的、带有数据访问功能的 UI 驱动站点。
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">了解详细信息...</a>
        </li>
        <li class="two">
            <h5>添加 NuGet 程序包并快速开始编码</h5>
            通过 NuGet，可以轻松地安装和更新免费的库和工具。
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">了解详细信息...</a>
        </li>
        <li class="three">
            <h5>查找 Web 宿主</h5>
            你可以轻松找到所提供的功能和价格都适合你应用程序的 Web 宿主公司。
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">了解详细信息...</a>
        </li>
    </ol>
</asp:Content>