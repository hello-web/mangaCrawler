export default {
    namespaced: true,
    state: {
        providers: [],
        selectedProvider: null,
    },
    mutations: {
        pushProvider(state, provider) {
            state.providers.push(provider)
        },
        clearProvider(state) {
            state.providers = []
        },
        selectProvider(state, idx) {
            if (idx < state.providers.length)
                state.selectedProvider = state.providers[idx]
        }
    },
    getters: {
        providerId(state) {
            return state.selectedProvider.Id
        }
    },
    actions: {
        refreshProvider(context) {
            context.commit('clearProvider')

            CS.getProviders(x => {
                if (x == '')
                    return
                
                let providers = JSON.parse(x)
                
                providers.forEach(x => context.commit('pushProvider', x))

                context.commit('selectProvider', 0)
                context.dispatch('manga/refreshManga', {}, { root:true })
            })
        }
    }
}