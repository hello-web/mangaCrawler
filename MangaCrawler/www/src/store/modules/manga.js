export default {
    namespaced: true,
    state: {
        mangaCurrent: null,
        mangaList: [],
        page: 1,
        maxPage: 0,
    },
    mutations: {
        clearManga(state) {
            state.mangaList = []
        },
        pushManga(state, item) {
            state.mangaList.push(item)
        },
        setCurrentManga(state, manga) {
            state.mangaCurrent = manga
        },
        setMaxPage(state, page) {
            state.maxPage = page
        }
    },
    getters: {
        mangalist(state) {
            return state.mangaList
        },
        mangacurrent(state) {
            if (state.mangaCurrent != null)
                return state.mangaCurrent
            return {}
        },
        mangaId(state) {
            if (state.mangaCurrent != null)
                return state.mangaCurrent.Id
            return null
        }
    },
    actions: {
        refreshManga(context) {
            context.commit('clearManga')

            let currentPage = context.state.page
            let currentProvider = context.rootGetters['provider/providerId']
            
            CS.getMangaList(currentProvider, currentPage, true, x => {
                if (x == '')
                    return
                
                let data = JSON.parse(x)
                let items = data.data
                
                items.forEach(x => context.commit('pushManga', x))
                context.commit('setMaxPage', data.maxPage)
            })
        },
        nextPage(context) {
            let page = context.state.page + 1
            context.dispatch('goPage', page)
        },
        prevPage(context) {
            let page = context.state.page - 1
            context.dispatch('goPage', page)
        },
        goPage(context, page) {
            let maxPage = context.state.maxPage
            let curPage = context.state.page
            if (page < 1 || page > maxPage || page == curPage)
                return              //ignore action
            
            context.state.page = page
            context.dispatch('refreshManga')
        }
    }
}