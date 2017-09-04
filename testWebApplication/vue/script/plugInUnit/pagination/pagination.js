(function () {
    var tem = '<div class="pagination">' +
				'<ul>' +
					'<li @click="controlPage(curPage-1)"><a class="lastPage" :class="{darkLight: curPage === 1}">上一页</a></li>' +
					'<li v-for="page in pages" @click="controlPage(page)">' +
						'<a class="page" :class="{highLight: curPage === page}" >{{ page }}</a>' +
					'</li>' +
					'<li @click="controlPage(curPage+1)"><a :class="{darkLight: curPage === totalPages}" class="nextPage">下一页</a></li>' +
				'</ul>' +
				'<p>现在是第{{curPage}}页</p>' +
			  '</div>'
    var pagination = Vue.extend({
        template: tem,
        props: {
            showPages: {
                type: Number,
                default: 5,
                required: true
            },
            totalPages: {
                type: Number,
                default: 20,
                required: true
            }
        },
        data: function () {
            return {
                curPage: 1
            };
        },
        computed: {
            pages: function () {
                var left = 1;
                var right = this.totalPages;
                var movePoint = Math.ceil(this.showPages / 2);
                var pages = [];
                if (this.curPage > movePoint && this.curPage < this.totalPages - movePoint + 1) {
                    left = this.showPages % 2 === 0 ? this.curPage - movePoint : this.curPage - movePoint + 1;
                    right = this.curPage + movePoint - 1;
                } else if (this.curPage <= movePoint) {
                    left = 1;
                    right = this.showPages;
                } else {
                    left = this.totalPages - this.showPages + 1;
                    right = this.totalPages;
                }

                while (left <= right) {
                    pages.push(left);
                    left++;
                }
                return pages;
            }
        },
        methods: {
            controlPage: function (page) {
                if (page > this.totalPages) {
                    return false;
                } else if (page < 1) {
                    return false;
                }
                if (this.curPage != page) {
                    this.curPage = page;
                }
            }
        }
    })

    window.pagination = pagination;
})();