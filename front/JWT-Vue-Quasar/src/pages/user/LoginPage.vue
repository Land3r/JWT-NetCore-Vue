<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-publiccard>
        <q-card-section class="bg-primary text-white">
          <div class="text-h6"><q-img src="statics/app-logo-128x128.png" class="on-left" style="width:32px; height:32px;" left/>{{$t('loginpage.title')}}</div>
        </q-card-section>
        <q-separator />
        <q-card-section>
          <q-form @submit="doLogin">
            <q-input color="primary" type="text" v-model="form.username" :label="$t('loginpage.form.username')" clearable clear-icon="close">
              <template v-slot:prepend>
                <q-icon name="perm_identity" />
              </template>
            </q-input>
            <q-space />
            <q-input color="primary" v-model="form.password" :label="$t('loginpage.form.password')" :type="showPassword ? 'text' : 'password'" >
              <template v-slot:append>
                <q-icon
                  :name="showPassword ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="showPassword = !showPassword"
                />
              </template>
            </q-input>
            <br/>
            <q-btn class="bg-primary text-white full-width" type="submit" @click="doLogin" :loading="isLoading" :disable="isLoading || !isFormValid">{{$t('loginpage.btn.login')}}</q-btn>
          </q-form>
          <div class="text-center q-my-xs text-uppercase">{{$t('loginpage.text.or')}}</div>
          <q-btn class="bg-secondary text-white full-width" :to="{ name: 'RegisterPage' }">{{$t('loginpage.btn.register')}}</q-btn>
          <div class="text-center q-mt-md q-mb-xs">
            <router-link :to="{ name: 'ForgotPasswordPage' }">{{$t('loginpage.btn.forgotpassword')}}</router-link>
          </div>
        </q-card-section>
      </app-publiccard>
    </app-transition>
  </q-page>
</template>

<script>
import xss from 'xss'
import { NotifySuccess, NotifyFailure } from 'data/notify'

import UserService from 'services/UserService'
import PublicCard from 'components/layout/PublicCard'
import Transition from 'components/common/presentation/Transition'

export default {
  name: 'LoginPage',
  components: {
    'app-transition': Transition,
    'app-publiccard': PublicCard
  },
  data: function () {
    return {
      isLoading: false,
      showPassword: false,
      form: {
        username: '',
        password: ''
      }
    }
  },
  mounted: function () {
    // If the user hits this page and is already loggedin, we should set the token for requests and redirects him to his homepage.
    const userservice = new UserService()
    if (userservice.isConnected()) {
      const user = userservice.getUser()
      const token = userservice.getToken()

      userservice.connect({ ...user, token: token, password: null })
      this.$router.push({ name: 'IndexPage' })
    }
  },
  computed: {
    isFormValid: function () {
      return this.form.username != null && this.form.username.length !== 0 && this.form.password != null && this.form.password.length !== 0
    }
  },
  methods: {
    doLogin: function () {
      this.isLoading = true

      const userservice = new UserService()
      userservice.doAuthenticate(this.form.username, this.form.password).then((response) => {
        userservice.connect(response)
        this.isLoading = false
        this.$q.notify({ ...NotifySuccess, message: this.$t('loginpage.success.loginsuccess', { username: xss(response.username) }), html: true })
        this.$router.push({ name: 'IndexPage' })
      }).catch((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifyFailure, message: this.$t('loginpage.error.loginfailure') })
      })
    }
  }
}
</script>
