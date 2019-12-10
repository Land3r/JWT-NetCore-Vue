<template>
  <q-layout view="hHh lpR fff">
    <q-header elevated class="app-header">
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          @click="leftDrawerOpen = !leftDrawerOpen"
          icon="menu"
          aria-label="Menu"
        />

        <q-toolbar-title>
          {{title}}
        </q-toolbar-title>

        <div>Quasar v{{ $q.version }}</div>
        <div>
          <q-btn flat>
            <q-avatar>
              <q-icon name="perm_identity" />
            </q-avatar>
            <q-menu
              transition-show="jump-down"
              transition-hide="jump-up"
            >
              <q-list style="min-width: 100px">
                <q-item clickable>
                  <q-item-section avatar>
                    <q-icon color="primary" name="perm_identity" />
                  </q-item-section>
                  <q-item-section>{{$t('mainlayout.user.profile')}}</q-item-section>
                </q-item>
                <q-separator />
                <q-item clickable @click="doDisconnect">
                  <q-item-section avatar>
                    <q-icon color="negative" name="close" />
                  </q-item-section>
                  <q-item-section>{{$t('mainlayout.user.disconnect')}}</q-item-section>
                </q-item>
              </q-list>
            </q-menu>
          </q-btn>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
      content-class="app-menu"
    >
      <q-list>
        <q-item-label header>JWT-NetCore-Vue</q-item-label>
          <q-item clickable tag="a" target="_blank" href="https://github.quasar.dev">
          <q-item-section avatar>
            <q-icon name="code" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Github</q-item-label>
            <q-item-label caption>github.com/Land3r/JWT-NetCore-Vue</q-item-label>
          </q-item-section>
        </q-item>
        <q-item-label header>Quasar Links</q-item-label>
        <q-item clickable tag="a" target="_blank" href="https://quasar.dev">
          <q-item-section avatar>
            <q-icon name="school" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Docs</q-item-label>
            <q-item-label caption>quasar.dev</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://github.quasar.dev">
          <q-item-section avatar>
            <q-icon name="code" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Github</q-item-label>
            <q-item-label caption>github.com/quasarframework</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://chat.quasar.dev">
          <q-item-section avatar>
            <q-icon name="chat" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Discord Chat Channel</q-item-label>
            <q-item-label caption>chat.quasar.dev</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://forum.quasar.dev">
          <q-item-section avatar>
            <q-icon name="record_voice_over" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Forum</q-item-label>
            <q-item-label caption>forum.quasar.dev</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://twitter.quasar.dev">
          <q-item-section avatar>
            <q-icon name="rss_feed" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Twitter</q-item-label>
            <q-item-label caption>@quasarframework</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://facebook.quasar.dev">
          <q-item-section avatar>
            <q-icon name="public" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Facebook</q-item-label>
            <q-item-label caption>@QuasarFramework</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import UserService from 'services/UserService'

export default {
  name: 'MainLayout',
  data () {
    return {
      leftDrawerOpen: false
    }
  },
  computed: {
    title: function () {
      return process.env.WEBSITE_NAME
    }
  },
  methods: {
    doDisconnect: function () {
      var userService = new UserService()
      userService.disconnect()
      this.$router.push({ name: 'LoginPage' })
    }
  }
}
</script>

<style>
.app-header {
  background: linear-gradient(145deg,#027be3 11%,#014a88 75%);
}

.app-menu {
  background-image: linear-gradient(rgba(245, 245, 245, 1), rgba(245, 245, 245, 0.75)), url("~assets/space.jpg");
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
}
</style>
