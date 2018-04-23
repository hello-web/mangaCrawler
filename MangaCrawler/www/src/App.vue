<template>
    <div id="app">
        <div class="page-wrapper">
            <app-header></app-header>
            <div class="clearfix"> </div>
            <div class="page-container">
                <app-sidebar></app-sidebar>
                <div class="page-content-wrapper">
                    <div class="page-content">
                        <app-breadcrumb></app-breadcrumb>
                        <app-title></app-title>
                        <router-view></router-view>
                    </div>
                </div>
            </div>
            <app-footer></app-footer>
        </div>
    </div>
</template>

<script>
import AppHeader from './components/Header.vue'
import AppFooter from './components/Footer.vue'
import AppSidebar from './components/Sidebar.vue'
import AppBreadcrumb from './components/Breadcrumb.vue'
import AppTitle from './components/Title.vue'

export default {
    name: 'app',
    components: {
        AppHeader,
        AppFooter,
        AppSidebar,
        AppBreadcrumb,
        AppTitle,
    },
    mounted() {
        this.$store.dispatch('provider/refreshProvider')
    }
}
</script>
