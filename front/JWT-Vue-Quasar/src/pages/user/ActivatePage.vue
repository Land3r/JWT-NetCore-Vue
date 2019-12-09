<template>
  <span></span>
</template>

<script>
import { NotifySuccess, NotifyFailure } from 'data/notify'

import UserService from 'services/UserService'

export default {
  name: 'ActivatePage',
  components: {
  },
  data: function () {
    return {
      activationtoken: ''
    }
  },
  mounted: function () {
    if (this.$route.params.activationtoken) {
      this.activationtoken = this.$route.params.activationtoken

      const userservice = new UserService()
      userservice.doActivate(this.activationtoken).then((response) => {
        this.$q.notify({ ...NotifySuccess, message: this.$t('activatepage.success.activatesuccess') })
        this.$router.push({ name: 'LoginPage' })
      }).catch((response) => {
        this.$q.notify({ ...NotifyFailure, message: this.$t('activatepage.error.activatefailure') })
        this.$router.push({ name: 'LoginPage' })
      })
    } else {
      this.$q.notify({ ...NotifyFailure, message: this.$t('activatepage.error.activatetokennotfound') })
    }
  }
}
</script>
