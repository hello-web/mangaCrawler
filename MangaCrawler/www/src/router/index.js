import Vue from 'vue'
import Router from 'vue-router'
import HomePage from '../pages/HomePage.vue'
import ChapterPage from '../pages/ChapterPage.vue'

Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: HomePage
        },
        {
            path: '/chapter/:id',
            name: 'chapter',
            component: ChapterPage
        }
    ]
})
