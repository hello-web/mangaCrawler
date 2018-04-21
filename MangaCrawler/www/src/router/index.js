import Vue from 'vue'
import Router from 'vue-router'
import HomePage from '../pages/HomePage.vue'
import ChapterPage from '../pages/ChapterPage.vue'
import ReadPage from '../pages/ReadPage.vue'

Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: HomePage
        },
        {
            path: '/chapter/:manga',
            name: 'chapter',
            component: ChapterPage
        },
        {
            path: '/chapter/:chapter/read',
            name: 'read',
            component: ReadPage
        }
    ]
})
