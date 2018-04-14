<template>
    <div id="app">
        <div class="page-wrapper">
            <app-header></app-header>
            <div class="clearfix"> </div>
            <div class="page-container">
                <app-sidebar></app-sidebar>
                <div class="page-content-wrapper">
                    <div class="page-content">
                        <router-view></router-view>
                        <div class="help-text">
                            <p>Vue.js 2 Single Page App (SPA) Example with vuex and vue-router.</p>
                            <a href="https://github.com/skyronic/vue-spa">View Source Code</a>
                        </div>
                    </div>
                </div>
            </div>
            <app-footer></app-footer>
        </div>
    </div>
</template>

<script>
import Hello from './components/Hello.vue'
import AppHeader from './components/Header.vue'
import AppFooter from './components/Footer.vue'
import AppSidebar from './components/Sidebar.vue'

export default {
  name: 'app',
  components: {
    Hello,
    AppHeader,
    AppFooter,
    AppSidebar,
  }
}
</script>
