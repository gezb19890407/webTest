
var app = new Vue({
    el: "#form",
    data: {
        fileName: "jquery.json-2.4.min.js",
        fileType: "JScript Script文件",
        fileCreateTime: "2017‎年‎8‎月‎9‎日，‏‎9:00:29",
        fileUpdateTime: "‎2017‎年‎6‎月‎16‎日，‏‎18:07:04",
        fileSize: "2.20 KB (2,259 字节)"
    }//,
    //methods: {
    //    reverseMessage: function () {
    //        this.message = this.message.split('').reverse().join('');
    //    }
    //},
    //filters: {
    //    capitalize: function (value) {
    //        if (!value) return "";
    //        value = value.toString();
    //        return value.charAt(0).toUpperCase() + value.slice(1);
    //    }
    //}
});

//window.open("fileManage.html");

//Vue.component("page", {
//    template: "#page",
//    data: function () {
//        return {
//            current: 1,
//            showItem: 5,
//            allpage: 13
//        }
//    },
//    computed: {
//        pages: function () {
//            var pag = [];
//            if (this.current < this.showItem) { //如果当前的激活的项 小于要显示的条数
//                //总页数和要显示的条数那个大就显示多少条
//                var i = Math.min(this.showItem, this.allpage);
//                while (i) {
//                    pag.unshift(i--);
//                }
//            } else { //当前页数大于显示页数了
//                var middle = this.current - Math.floor(this.showItem / 2),//从哪里开始
//                    i = this.showItem;
//                if (middle > (this.allpage - this.showItem)) {
//                    middle = (this.allpage - this.showItem) + 1
//                }
//                while (i--) {
//                    pag.push(middle++);
//                }
//            }
//            return pag
//        }
//    },
//    methods: {
//        goto: function (index) {
//            if (index == this.current) return;
//            this.current = index;
//            //这里可以发送ajax请求
//        }
//    }
//});

//var vm = new Vue({
//    el: '#app'
//});


var dataGrid = new Vue({
    el: '#dataGrid',
    data: {
        entityList: [
            {
                fileName: "ComplaintList.html",
                fileType: "HTML 文档",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "9KB"
            }, {
                fileName: "ComplaintList.html.js",
                fileType: "JScript Script文件",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "6KB"
            }, {
                fileName: "DeliverOrder.html",
                fileType: "HTML 文档",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "4KB"
            }, {
                fileName: "DeliverOrder.html.js",
                fileType: "JScript Script文件",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "1KB"
            }, {
                fileName: "HolidayManager.html",
                fileType: "HTML 文档",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "3KB"
            }, {
                fileName: "HolidayManager.html.js",
                fileType: "JScript Script文件",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "7KB"
            }, {
                fileName: "Page__Petition.html",
                fileType: "HTML 文档",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "6KB"
            }, {
                fileName: "Page__Petition.html.js",
                fileType: "JScript Script文件",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "13KB"
            }, {
                fileName: "Page__Petition_InspectRecord.html",
                fileType: "HTML 文档",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "2KB"
            }, {
                fileName: "Page__Petition_InspectRecord.html.js",
                fileType: "JScript Script文件",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "6KB"
            }, {
                fileName: "Page__PetitionImport.html",
                fileType: "HTML 文档",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "6KB"
            }, {
                fileName: "Page__PetitionImport.html.js",
                fileType: "JScript Script文件",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "7KB"
            }, {
                fileName: "Petition__Combine.html",
                fileType: "HTML 文档",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "4KB"
            }, {
                fileName: "Petition__Combine.html.js",
                fileType: "JScript Script文件",
                fileCreateTime: "2017-06-16",
                fileUpdateTime: "2017-06-16",
                fileSize: "15KB"
            }
        ]
    },
    methods: {

    }
});


//Vue.component("paging", {
//    template: function () {
//        html = "";
//        html += "<div class=\"pager\" id=\"pager\">";
//        html += "每页<span class=\"form-inline\">";
//        html += "    <select class=\"form-control\" v-model=\"pagesize\" v-on:change=\"showPage(pageCurrent,$event,true)\" number>";
//        html += "        <option value=\"10\">10</option>";
//        html += "        <option value=\"20\">20</option>";
//        html += "        <option value=\"30\">30</option>";
//        html += "        <option value=\"40\">40</option>";
//        html += "    </select>";
//        html += "</span>";
//        html += "条,转到<span class=\"form-inline\">";
//        html += "    <input class=\"pageIndex form-control\" style=\"width:60px;text-align:center\"";
//        html += "           type=\"text\" v-model=\"pageCurrent | onlyNumeric\" v-on:keyup.enter=\"showPage(pageCurrent,$event,true)\"";
//        html += "           v-on:blur=\"showPage(pageCurrent,$event,true)\" />";
//        html += "</span>页";
//        html += "<span>{{pageCurrent}}/{{pageCount}}</span>";
//        html += "<template v-for=\"item in pageCount+1\">";
//        html += "    <span v-if=\"item==1\" class=\"btn btn-default\" v-on:click=\"showPage(1,$event)\">";
//        html += "        首页";
//        html += "    </span>";
//        html += "    <span v-if=\"item==1\" class=\"btn btn-default\" v-on:click=\"showPage(pageCurrent-1,$event)\">";
//        html += "        上一页";
//        html += "    </span>";
//        html += "    <span v-if=\"item==1\" class=\"btn btn-default\" v-on:click=\"showPage(item,$event)\">";
//        html += "{{item}}";
//        html += "        </span>";
//        html += "        <span v-if=\"item==1&&item<showPagesStart-1\" class=\"btn btn-default disabled\">";
//        html += "            ...";
//        html += "        </span>";
//        html += "        <span v-if=\"item>1&&item<=pageCount-1&&item>=showPagesStart&&item<=showPageEnd&&item<=pageCount\" class=\"btn btn-default\" v-on:click=\"showPage(item,$event)\">";
//        html += "{{item}}";
//        html += "        </span>";
//        html += "        <span v-if=\"item==pageCount&&item>showPageEnd+1\" class=\"btn btn-default disabled\">";
//        html += "            ...";
//        html += "         </span>";
//        html += "        <span v-if=\"item==pageCount\" class=\"btn btn-default\" v-on:click=\"showPage(item,$event)\">";
//        html += "{{item}}";
//        html += "        </span>";
//        html += "        <span v-if=\"item==pageCount\" class=\"btn btn-default\" v-on:click=\"showPage(pageCurrent+1,$event)\">";
//        html += "            下一页";
//        html += "        </span>";
//        html += "        <span v-if=\"item==pageCount\" class=\"btn btn-default\" v-on:click=\"showPage(pageCount,$event)\">";
//        html += "            尾页";
//        html += "        </span>";
//        html += "    </template>";
//        html += "</div>";
//        return html;
//    },
//    data: {
//        //总项目数
//        totalCount: 200,
//        //分页数
//        pageCount: 20,
//        //当前页面
//        pageCurrent: 1,
//        //分页大小
//        pagesize: 10,
//        //显示分页按钮数
//        showPages: 11,
//        //开始显示的分页按钮
//        showPagesStart: 1,
//        //结束显示的分页按钮
//        showPageEnd: 100,
//        //分页数据
//        arrayData: []
//    },
//    computed: {
//        pages: function () {
//            var pag = [];
//            if (this.current < this.showItem) { //如果当前的激活的项 小于要显示的条数
//                //总页数和要显示的条数那个大就显示多少条
//                var i = Math.min(this.showItem, this.allpage);
//                while (i) {
//                    pag.unshift(i--);
//                }
//            } else { //当前页数大于显示页数了
//                var middle = this.current - Math.floor(this.showItem / 2),//从哪里开始
//                    i = this.showItem;
//                if (middle > (this.allpage - this.showItem)) {
//                    middle = (this.allpage - this.showItem) + 1
//                }
//                while (i--) {
//                    pag.push(middle++);
//                }
//            }
//            return pag
//        }
//    },
//    methods: {
//        //分页方法
//        showPage: function (pageIndex, $event, forceRefresh) {

//            if (pageIndex > 0) {


//                if (pageIndex > this.pageCount) {
//                    pageIndex = this.pageCount;
//                }

//                //判断数据是否需要更新
//                var currentPageCount = Math.ceil(this.totalCount / this.pagesize);
//                if (currentPageCount != this.pageCount) {
//                    pageIndex = 1;
//                    this.pageCount = currentPageCount;
//                }
//                else if (this.pageCurrent == pageIndex && currentPageCount == this.pageCount && typeof (forceRefresh) == "undefined") {
//                    console.log("not refresh");
//                    return;
//                }

//                //处理分页点中样式
//                var buttons = $("#pager").find("span");
//                for (var i = 0; i < buttons.length; i++) {
//                    if (buttons.eq(i).html() != pageIndex) {
//                        buttons.eq(i).removeClass("active");
//                    }
//                    else {
//                        buttons.eq(i).addClass("active");
//                    }
//                }

//                //测试数据 随机生成的
//                var newPageInfo = [];
//                for (var i = 0; i < this.pagesize; i++) {
//                    newPageInfo[newPageInfo.length] = {
//                        name: "test" + (i + (pageIndex - 1) * 20),
//                        age: (i + (pageIndex - 1) * 20)
//                    };
//                }
//                this.pageCurrent = pageIndex;
//                this.arrayData = newPageInfo;

//                //计算分页按钮数据
//                if (this.pageCount > this.showPages) {
//                    if (pageIndex <= (this.showPages - 1) / 2) {
//                        this.showPagesStart = 1;
//                        this.showPageEnd = this.showPages - 1;
//                        console.log("showPage1")
//                    }
//                    else if (pageIndex >= this.pageCount - (this.showPages - 3) / 2) {
//                        this.showPagesStart = this.pageCount - this.showPages + 2;
//                        this.showPageEnd = this.pageCount;
//                        console.log("showPage2")
//                    }
//                    else {
//                        console.log("showPage3")
//                        this.showPagesStart = pageIndex - (this.showPages - 3) / 2;
//                        this.showPageEnd = pageIndex + (this.showPages - 3) / 2;
//                    }
//                }
//                console.log("showPagesStart:" + this.showPagesStart + ",showPageEnd:" + this.showPageEnd + ",pageIndex:" + pageIndex);
//            }

//        },
//        deleteItem: function (index, age) {
//            if (confirm('确定要删除吗')) {
//                //console.log(index, age);

//                var newArray = [];
//                for (var i = 0; i < this.arrayData.length; i++) {
//                    if (i != index) {
//                        newArray[newArray.length] = this.arrayData[i];
//                    }
//                }
//                this.arrayData = newArray;
//            }
//        }
//    }
//});

//var vm = new Vue({
//    el: '#paging'
//});

//vue.showPage(vue.pageCurrent, null, true);

var pager = new Vue({
    el: "#pager",
    data: {
        showPages: 5,
        totalPages: 20
    },
    components: {
        'pagination': pagination
    }
});

pagination.extendOptions.methods.controlPage(1);
//http://www.jq22.com/demo/pagination_vue201702280056