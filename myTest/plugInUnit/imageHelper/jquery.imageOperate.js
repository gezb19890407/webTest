/*
add by gezb 2017-07-13 图片操作帮助类

提供图片展开、收起、全屏、左转、右转
提供html最大化
*/

(function ($) {
    var imageType = ["bmp", "jpg", "png", "tiff", "gif", "pcx", "tga", "exif", "fpx", "svg", "psd", "cdr", "pcd", "dxf", "ufo", "eps", "ai", "raw", "WMF"];
    var audioType = ["mp3", "wma", "wav", "aac", "mp3pro", "vqf", "flac", "ape", "mid", "ogg"];
    var videoType = ["mpeg", "avi", "rm", "asf", "wmv", "mov", "mp4"];
    var htmlType = ["htm", "html", "shtml", "stm", "shtm", "asp"];
    var fileType = {
        imageType: 1,
        audioType: 2,
        videoType: 3,
        documentType: 4,
        htmlType: 5
    };

    Array.prototype.contains = function (obj) {
        var i = this.length;
        while (i--) {
            if (this[i] === obj) {
                return true;
            };
        };
        return false;
    };

    $.imageOperation = function (options, elm) {
        this.options = $.extend({}, this.defultOptions, options);
        this.options.parentJQ = elm;
        this.init();
    };

    $.imageOperation.prototype = {
        defultOptions: {
            dataSource: [],
            parentJQ: null,
            urlField: "url",
            fileNameField: "fileName",
            htmlType: null,
            thumbnailUrl: "src",
            originalUrl: "src",
            isRequestImg: true,                                                       //isRequestImg：true：image false：html
            unImageSrc: null,                                                         //unImageSrc："../../styles/image/imgUnload.png"
            fadeInMillisecond: 200                                                    //fadeInMillisecond：淡出毫秒数
        },
        options: {},
        init: function () {
            this.initOptions();
            this.initImageDom();
            this.loadErrorEvent();
            this.loadClickEvent();
        },
        privateOptions: {
            unImageSrc: null,
            defaultRecordSrc: null,
            defaultRecordPreSrc: null,
            defaultPrintEvidenceListSrc: null,
            defaultPrintEvidenceListPreSrc: null,
            defaultPrintEvidenceSrc: null,
            defaultPrintEvidencePreSrc: null
        },
        initImageDom: function () {
            var imageFileType, url, imageUrl, fileName, fileExtension, defaultSrc, imageHtml = "";
            for (var i = 0, len = this.options.dataSource.length; i < len; i++) {
                imageUrl = this.options.dataSource[i][this.options.urlField];
                url = imageUrl;
                fileName = this.options.dataSource[i][this.options.fileNameField];
                if (fileName && fileName.length > 0) {
                    fileExtension = fileName.substring(fileName.lastIndexOf(".") + 1).toLowerCase();
                }
                else {
                    fileExtension = imageUrl.substring(imageUrl.lastIndexOf(".") + 1).toLowerCase();
                };
                defaultSrc = this.privateOptions.unImageSrc;
                if (imageType.contains(fileExtension)) {
                    imageFileType = fileType.imageType;
                } else if (audioType.contains(fileExtension)) {
                    imageFileType = fileType.audioType;
                } else if (videoType.contains(fileExtension)) {
                    imageFileType = fileType.videoType;
                }
                else if (htmlType.contains(fileExtension)) {
                    imageFileType = fileType.htmlType;
                    /***软件私有方法***/
                    switch (this.options.htmlType) {
                        case 2:
                            defaultSrc = this.privateOptions.defaultPrintEvidenceListSrc;
                            break;
                        case 3:
                            defaultSrc = this.privateOptions.defaultPrintEvidenceSrc;
                            break;
                        default: defaultSrc = this.privateOptions.defaultRecordSrc;
                    }
                    /***软件私有方法***/
                    imageUrl = defaultSrc;
                } else {
                    imageFileType = fileType.documentType;
                    imageUrl = defaultSrc;
                };
                imageHtml += "<div class='imageOperate_BorderDiv'><img imageFileType = '" + imageFileType
                            + "' data-Entity = '" + this.options.dataSource[i] + "' htmlViewUrl='" + url
                            + "' class='imageOperate_BorderImage' defaultSrc='" + defaultSrc + "' src='" + imageUrl + "'  /></div>";
            };
            $(imageHtml).appendTo(this.options.parentJQ);
        },
        initOptions: function () {
            this.privateOptions.unImageSrc = window.webSiteCurrentUrl + "image/imgUnload.png";
            this.privateOptions.defaultRecordSrc = window.webSiteCurrentUrl + "image/record.png";
            this.privateOptions.defaultRecordPreSrc = window.webSiteCurrentUrl + "image/recordClick.png";
            this.privateOptions.defaultPrintEvidenceListSrc = window.webSiteCurrentUrl + "image/evidenceList.png";
            this.privateOptions.defaultPrintEvidenceListPreSrc = window.webSiteCurrentUrl + "image/evidenceListClick.png";
            this.privateOptions.defaultPrintEvidenceSrc = window.webSiteCurrentUrl + "image/evidence.png";
            this.privateOptions.defaultPrintEvidencePreSrc = window.webSiteCurrentUrl + "image/evidenceClick.png";

        },
        loadClickEvent: function () {
            var thisElem = this;
            $("img", this.options.parentJQ).load(function () {
                $(this).unbind("click").bind("click", function () {
                    var thisJQ = $(this);
                    var htmlViewUrl = thisJQ.attr("htmlViewUrl");
                    var imageFileType = thisJQ.attr("imageFileType");
                    switch (parseInt(imageFileType)) {
                        case fileType.imageType:
                            thisElem.getImgToolBar.getExpandImg(thisJQ, thisElem);
                            break;
                        case fileType.audioType:
                        case fileType.videoType:
                            thisElem.setDownLoadClickEvent(thisJQ, htmlViewUrl);
                            break;
                        case fileType.documentType: break;
                        case fileType.htmlType: thisElem.setHtmlClickEvent(thisJQ, htmlViewUrl);
                            break;
                        default: thisElem.setDownLoadClickEvent(thisJQ, htmlViewUrl); ;
                    };
                });
            });
        },
        loadErrorEvent: function () {
            var thisElem = this;
            $('img').error(function () {
                $(this).attr('src', $(this).attr("defaultSrc")).attr("isRequestImg", true);
                $(this).hover(function () {
                    if ($(".imageOperate_OverLoad") && $(".imageOperate_OverLoad").length > 0) {
                        $(".imageOperate_OverLoad").remove();
                    };
                    var h = "";
                    h += '<div title="重新加载" class="imageOperate_OverLoad" style="display:block;"></div>';
                    $(this).after(h);
                    $(".imageOperate_OverLoad").mouseout(function () {
                        $(".imageOperate_OverLoad").remove();
                    })
                });
            });
        },
        setHtmlClickEvent: function (imgJQ) {
            var html = "";
            var imgUrl = imgJQ.attr("src");
            var htmlViewUrl = imgJQ.attr("htmlViewUrl");
            if (!$(".imageOperate_FullScreen") || $(".imageOperate_FullScreen").length < 1) {
                html += '   <div  class="imageOperate_FullScreen">';
                html += '   <div  class="imageOperate_CloseImg"></div>';
                html += '   <div  class="imageOperate_FullScreenImg">';
                html += '   </div>';
                html += '<div class="imageOperate_IframeDiv"><iframe class="imageOperate_QueryIframe" src=""></iframe></div>';
                html += '  </div>';
                $(html).appendTo("body");
            } else {
                $(".imageOperate_FullScreen").show();
            };
            var FullScreenImgJQ = $(".imageOperate_FullScreenImg");
            var queryRecordIframeJQ = $(".imageOperate_QueryIframe");
            var iframeDivJQ = $(".imageOperate_IframeDiv");
            if (htmlViewUrl) {
                queryRecordIframeJQ.attr("src", htmlViewUrl);
                FullScreenImgJQ.hide();
                iframeDivJQ.fadeIn(this.options.fadeInMillisecond);
            } else if (imgUrl) {
                FullScreenImgJQ.css({ "background": "url('" + imgUrl + "') no-repeat center center ", "background-size": " 800px 600px" });
                iframeDivJQ.hide();
                FullScreenImgJQ.fadeIn(this.options.fadeInMillisecond);
            };

            $(".imageOperate_FullScreen").unbind("click").bind("click", function () {
                $(".imageOperate_FullScreen").hide();
            });
            $(".imageOperate_CloseImg").unbind("click").bind("click", function () {
                $(".imageOperate_FullScreen").hide();
            });
        },
        setDownLoadClickEvent: function () {

        },
        getImgToolBar: {
            getExpandImg: function (imgThisJQ, thisElem) {
                var globalObject = thisElem;
                var pImgThisJQ = imgThisJQ.parent();
                var ppImgThisJQ = imgThisJQ.parent().parent();
                var pppImgThisJQ = ppImgThisJQ.parent();
                changeImgActiveStatus();
                getExpandImgToolBarContent();
                function changeImgActiveStatus() {
                    $(".imageOperate_ContentNoticeImageActive", ppImgThisJQ).removeClass("imageOperate_ContentNoticeImageActive");
                    pImgThisJQ.addClass("imageOperate_ContentNoticeImageActive");
                };
                function getExpandImgToolBarContent() {
                    removeExistExpandImgDiv();
                    setContentHtml();
                    globalObject.getImgToolBar.getImgPackUp(pppImgThisJQ);
                    globalObject.getImgToolBar.getImgTurn(pppImgThisJQ);
                    globalObject.getImgToolBar.getFullScreen(pppImgThisJQ);
                    function removeExistExpandImgDiv() {
                        var expandImgDivJQ = $(".imageOperate_ContentNoticeExpandDiv", pppImgThisJQ);
                        if (expandImgDivJQ && expandImgDivJQ.length > 0) {
                            expandImgDivJQ.remove();
                        };
                    };
                    function setContentHtml() {
                        var html = "";
                        var imgUrl = imgThisJQ.attr("src");
                        var offsetLeft = globalObject.options.parentJQ[0].offsetLeft;
                        html += '  <div style=\"margin-top:5px\" class="imageOperate_ContentDiv imageOperate_ContentNoticeExpandDiv" ">';
                        html += '                 <div class="imageOperate_ContentText" >';
                        html += '                     <div class="imageOperate_ContentTextInfo imageOperate_ImagePackUp">';
                        html += '                         <span>收起</span>';
                        html += '                     </div>';
                        html += '                    <div  class="imageOperate_ContentTextInfo imageOperate_ImageFullScreen">';
                        html += '                       <span>全屏</span></div>';
                        html += '                   <div class="imageOperate_ContentTextInfo imageOperate_ImageTurnLeft">';
                        html += '                      <span>向左转</span></div>';
                        html += '                  <div  class="imageOperate_ContentTextInfo imageOperate_ImageTurnRight">';
                        html += '                     <span>向右转</span></div>';
                        html += '              </div>';
                        html += '              <img class="imageOperate_ContentNoticeExpandImg"   src="' + imgUrl + '" />';
                        html += '          </div>';
                        $(html).appendTo(pppImgThisJQ);
                        changeExpandImgDivWidth();
                    };
                    function changeExpandImgDivWidth() {
                        var expandImgDivJQ = $(".imageOperate_ContentNoticeExpandDiv", pppImgThisJQ);
                        if (!expandImgDivJQ || expandImgDivJQ.length < 1) {
                            return;
                        };
                        var width = $(".imageOperate_ContentNoticeExpandImg", pppImgThisJQ).width();
                        expandImgDivJQ.css({ width: width + "px" });
                    };
                };
            },
            getImgPackUp: function (divJQ, thisElem) {
                $(".imageOperate_ImagePackUp", divJQ).unbind("click").bind("click", function () {
                    $(".imageOperate_ContentNoticeExpandDiv", divJQ).hide();
                    $(".imageOperate_ContentNoticeImageActive", divJQ).removeClass("imageOperate_ContentNoticeImageActive");
                });
            },
            getImgTurn: function (divJQ, thisElem) {
                var deg = 0;
                var imgJQ = $(".imageOperate_ContentNoticeExpandImg", divJQ);
                if (!imgJQ || imgJQ.length < 1) {
                    return;
                };
                $(".imageOperate_ImageTurnLeft", divJQ).unbind("click").bind("click", function () {
                    rotateImg("left", imgJQ);
                });
                $(".imageOperate_ImageTurnRight", divJQ).unbind("click").bind("click", function () {
                    rotateImg("right", imgJQ);
                });
                function rotateImg(rotateType, imgJQ) {
                    if (!imgJQ || !rotateType) {
                        return false;
                    };
                    var step = getStep(imgJQ, rotateType);
                    canvasDrawImg(imgJQ, step);
                    function getStep(imgJQ, rotateType) {
                        var step = imgJQ.attr('step') ? parseInt(imgJQ.attr('step')) : 0;
                        if (rotateType === 'right') {
                            if (step >= 3) {
                                step = 0;
                            } else {
                                step++;
                            };
                        } else if (rotateType === 'left') {
                            if (step <= 0) {
                                step = 3;
                            } else {
                                step--;
                            };
                        };
                        imgJQ.attr('step', step);
                        return step;
                    };
                    function canvasDrawImg(imgJQ, step) {
                        var pImgJQ = imgJQ.parent();
                        var height = imgJQ.height();
                        var width = imgJQ.width();
                        var canvasJQ = $('#canvas', pImgJQ);
                        if (!canvasJQ || canvasJQ.length < 1) {
                            imgJQ.hide().css({ "position": "absolute" });
                            var html = "<canvas id='canvas'><p>浏览器不支持canvas</p></canvas>";
                            $(html).appendTo(pImgJQ);
                            canvasJQ = $('#canvas', pImgJQ);
                        };
                        if (!canvasJQ || canvasJQ.length < 1 || !canvasJQ[0].getContext) {
                            return;
                        };
                        var canvasContext = canvasJQ[0].getContext('2d');
                        var deg = 0,
                        canvasWidth = width,
                        canvasHeight = height;
                        pImgJQWidth = width,
                        x = 0,
                        y = 0;
                        switch (step) {
                            default:
                            case 0:
                                deg = step;
                                pImgJQWidth = width;
                                canvasWidth = width;
                                canvasHeight = height;
                                x = 0;
                                y = 0;
                                break;
                            case 1:
                                deg = step;
                                pImgJQWidth = height;
                                canvasWidth = height;
                                canvasHeight = width;
                                x = 0;
                                y = -height;
                                break;
                            case 2:
                                deg = step;
                                pImgJQWidth = width;
                                canvasWidth = width;
                                canvasHeight = height;
                                x = -width;
                                y = -height;
                                break;
                            case 3:
                                deg = step;
                                pImgJQWidth = height;
                                canvasWidth = height;
                                canvasHeight = width;
                                x = -width;
                                y = 0;
                                break;
                        };
                        imgJQ.attr("deg", deg * 90);
                        pImgJQ.css({ "width": pImgJQWidth });
                        canvasJQ.attr('width', canvasWidth);
                        canvasJQ.attr('height', canvasHeight);
                        canvasContext.rotate(deg * 90 * Math.PI / 180);
                        canvasContext.drawImage(imgJQ[0], x, y, width, height);
                    };
                };
            },
            getFullScreen: function (divJQ, thisElem) {
                $(".imageOperate_ImageFullScreen", divJQ).unbind("click").bind("click", function () {
                    var imgJQ = $(".imageOperate_ContentNoticeExpandImg", divJQ);
                    if (!imgJQ || imgJQ.length < 1) {
                        return;
                    };
                    if (!$("#imageOperate_FullScreen") || $("#imageOperate_FullScreen").length < 1) {
                        getFullScreenContent();
                    };
                    if ($("#imageOperate_FullScreen").is(":hidden")) {
                        $("#imageOperate_FullScreen").show();
                    };
                    changeFullScreenImgStyle(imgJQ);
                    closeFullScreen();
                    function getFullScreenContent() {
                        var html = "";
                        html += '   <div id="imageOperate_FullScreen" class="imageOperate_FullScreen">';
                        html += '   <div id="imageOperate_CloseImg" class="imageOperate_CloseImg"></div>';
                        html += '   <div  class="imageOperate_FullScreenImg">';
                        html += '   </div>';
                        html += '  </div>';
                        $(html).appendTo("body");
                    };
                    function changeFullScreenImgStyle(imgJQ) {
                        var width = (imgJQ.width() * 2 > 1000 ? 1000 : imgJQ.width() * 2) + "px";
                        var height = (imgJQ.height() * 2 > 800 ? 800 : imgJQ.height() * 2) + "px";
                        var imgUrl = imgJQ.attr("src");
                        var FullScreenImgJQ = $(".imageOperate_FullScreenImg");
                        var deg = imgJQ.attr("deg");
                        if (!deg) {
                            deg = 0;
                        };
                        var transform = "translate(0,-50%) rotate(" + deg + "deg)";
                        FullScreenImgJQ.css({
                            "background": "url('" + imgUrl + "') no-repeat center center ",
                            "background-size": width + " " + height,
                            "transform": transform,
                            "-ms-transform": transform,
                            "-moz-transform": transform,
                            "-webkit-transform": transform,
                            "-o-transform": transform
                        });
                        FullScreenImgJQ.fadeIn(200);
                    }
                    function closeFullScreen() {
                        $("#imageOperate_FullScreen").unbind("click").bind("click", function () {
                            $("#imageOperate_FullScreen").hide();
                        });
                        $("#imageOperate_CloseImg").unbind("click").bind("click", function () {
                            $("#imageOperate_FullScreen").hide();
                        });
                    };
                });
            }
        }
    };

    $.fn.imageOperation = function (options) {
        if (this.length > 0) {
            new $.imageOperation(options, this);
        };
    };

    var getWebSiteRootUrl = function () {
        var __CreateJSPath = function (js) {
            var scripts = document.getElementsByTagName("script");
            var path = "";
            for (var i = 0, l = scripts.length; i < l; i++) {
                var src = scripts[i].src;
                if (src.indexOf(js) != -1) {
                    var ss = src.split(js);
                    path = ss[0];
                    break;
                }
            }
            var href = location.href;
            href = href.split("#")[0];
            href = href.split("?")[0];
            var ss = href.split("/");
            ss.length = ss.length - 1;
            href = ss.join("/");
            if (path.indexOf("https:") == -1 && path.indexOf("http:") == -1 && path.indexOf("file:") == -1 && path.indexOf("\/") != 0) {
                path = href + "/" + path;
            }
            return path;
        };
        if (!window.webSiteCurrentUrl) {
            window.webSiteCurrentUrl = __CreateJSPath("jquery.imageOperate.js");
            window.webSiteRootUrl = window.webSiteCurrentUrl.replace("web/scripts/plugInUnit/image/", "");
        };
    };
    getWebSiteRootUrl();
    document.write('<link href="' + window.webSiteRootUrl + 'web/scripts/plugInUnit/image/jquery.imageOperate.css" rel="stylesheet" type="text/css" />');
})(jQuery);