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
        setManga(context, manga) {
            context.commit('setCurrentManga', manga);
        },
        refreshManga(context) {
            context.commit('clearManga')

            let currentPage = context.state.page
            let currentProvider = context.rootGetters['provider/providerId']
            
            CS.getMangaList(currentProvider, currentPage, true, x => {
                if (x == '')
                    return
                
                let data = JSON.parse(x)
                data.forEach(x => context.commit('pushManga', x))
            })
        }
    }
}