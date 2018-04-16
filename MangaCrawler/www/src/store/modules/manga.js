export default {
    namespaced: true,
    state: {
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
        }
    },
    getters: {
        mangalist(state) {
            return state.mangaList
        }
    },
    actions: {
        refreshManga(context) {
            context.commit('clearManga')

            let currentPage = context.state.page
            let currentProvider = context.rootGetters['provider/providerId']
            
            CS.getMangaList(currentProvider, currentPage, x => {
                if (x == '')
                    return
                
                let data = JSON.parse(x)
                data.forEach(x => context.commit('pushManga', x))
            })
        }
    }
}