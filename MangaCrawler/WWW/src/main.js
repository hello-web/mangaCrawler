// The following line loads the standalone build of Vue instead of the runtime-only build,
// so you don't have to do: import Vue from 'vue/dist/vue'
// This is done with the browser options. For the config, see package.json
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/index'
import { sync } from 'vuex-router-sync'

sync(store, router)

new Vue({ // eslint-disable-line no-new
  el: '#app',
  router,
  store,
  render: (h) => h(App)
})
