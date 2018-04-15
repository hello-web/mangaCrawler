<template>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li v-for="(br, idx) in arr_breadcrumb" :key="idx">
                <span>{{ br }}</span>
                <i class="fa fa-circle" v-if="idx != arr_len - 1"></i>
            </li>
        </ul>
        <div class="page-toolbar">
            <div class="btn-group pull-right">
                <button type="button" class="btn green btn-sm btn-outline dropdown-toggle" data-toggle="dropdown"> Actions
                    <i class="fa fa-angle-down"></i>
                </button>
                <ul class="dropdown-menu pull-right" role="menu">
                    <li>
                        <a href="#"><i class="icon-bell"></i> Action</a>
                    </li>
                    <li class="divider"></li>
                    <li>
                        <a href="#"><i class="icon-bag"></i> Separated link</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            arr_breadcrumb: ['Home']
        }
    },
    computed: {
        arr_len() {
            return this.arr_breadcrumb.length
        }
    },
    methods: {
        setTitle(param) {
            if ('breadcrumb' in param) {
                this.arr_breadcrumb = []
                this.arr_breadcrumb.push('Home')

                param.breadcrumb.forEach(x => this.arr_breadcrumb.push(x))
            }
        }
    },
    mounted() {
        // Bus On
        this.$bus.$on('set-title', this.setTitle)
    },
    beforeDestroy() {
        // Bus Off
        this.$bus.$off('set-title', this.setTitle)
    }
}
</script>
