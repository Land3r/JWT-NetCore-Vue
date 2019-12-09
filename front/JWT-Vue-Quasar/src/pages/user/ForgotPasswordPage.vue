<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-publiccard>
        <q-card-section class="bg-primary text-white">
          <div class="text-h6"><q-icon name="security" size="md" left/>{{$t('forgotpasswordpage.title')}}</div>
        </q-card-section>

        <q-separator />
        <q-card-section>
          <q-form>
            <q-input color="primary" type="text" v-model="form.username" :label="$t('forgotpasswordpage.form.username')" clearable clear-icon="close">
              <template v-slot:prepend>
                <q-icon name="perm_identity" />
              </template>
            </q-input>
            <div class="text-center q-mt-md text-uppercase">
              {{$t('forgotpasswordpage.text.or')}}
            </div>
            <q-input color="primary" v-model="form.email" :label="$t('forgotpasswordpage.form.email')" type="email" clearable clear-icon="close">
              <template v-slot:prepend>
                <q-icon name="mail" />
              </template>
            </q-input>
            <br />
            <div class="text-center">{{$t('forgotpasswordpage.text.description', {duration: 15})}}</div>
            <br />
            <q-btn class="bg-primary text-white full-width" type="submit" @click="doForgotPassword" :loading="isLoading" :disable="isLoading || !isFormValid">{{$t('forgotpasswordpage.btn.send')}}</q-btn>
          </q-form>
          <q-btn class="bg-secondary text-white full-width q-mt-md q-mb-xs" :to="{ name: 'LoginPage' }">{{$t('forgotpasswordpage.btn.login')}}</q-btn>
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
  name: 'ForgotPasswordPage',
  components: {
    'app-transition': Transition,
    'app-publiccard': PublicCard
  },
  data: function () {
    return {
      isLoading: false,
      form: {
        username: '',
        email: ''
      }
    }
  },
  computed: {
    isFormValid: function () {
      return (this.form.username != null && this.form.username.length !== 0) || (this.form.email != null && this.form.email.length !== 0)
    }
  },
  methods: {
    doForgotPassword: function () {
      this.isLoading = true

      const userservice = new UserService()
      userservice.doForgotPassword(this.form.username, this.form.email).then((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifySuccess, message: this.$t('forgotpasswordpage.success.sendsuccess', { username: xss(response.username) }), html: true })
      }).catch((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifyFailure, message: this.$t('forgotpasswordpage.error.sendfailure') })
      })
    }
  }
}
</script>
