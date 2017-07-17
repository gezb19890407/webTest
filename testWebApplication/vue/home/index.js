

Vue.component('todo-item', {
    props: ['entity'],
    template: "<li>{{entity.text}}</li>"
});

var app = new Vue({
    el: "#app",
    data: {
        message: "hello ",
        a: false,
        entityList: [
            { id: 1, text: "第1条" },
            { id: 2, text: "第2条" }
        ]
    },
    methods: {
        reverseMessage: function () {
            this.message = this.message.split('').reverse().join('');
        }
    },
    filters: {
        capitalize: function (value) {
            if (!value) return "";
            value = value.toString();
            return value.charAt(0).toUpperCase() + value.slice(1);
        }
    }
});